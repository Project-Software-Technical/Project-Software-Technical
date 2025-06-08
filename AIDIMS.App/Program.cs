using AIDIMS.Core.Data;
using AIDIMS.Repositories.Impl;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Impl;
using AIDIMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using AutoMapper;
using AIDIMS.App.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình để lắng nghe trên tất cả các địa chỉ IP
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5000); // HTTP
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        // Tạm thời tắt HTTPS để dễ truy cập, trong môi trường production nên bật lại và cấu hình chứng chỉ
        listenOptions.UseHttps();
    });
});

// Thêm cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Thêm cấu hình AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AIDIMS API",
        Version = "v1",
        Description = "API cho hệ thống quản lý thông tin chẩn đoán AI (AIDIMS)"
    });
});

// Đăng ký DbContext với PostgreSQL
builder.Services.AddDbContext<AIDIMSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
// Thêm các repository khác ở đây

// Đăng ký Services
builder.Services.AddScoped<IRoleService, RoleService>();
// Thêm các service khác ở đây

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AIDIMS API v1");
        // Đặt Swagger UI là trang mặc định
        c.RoutePrefix = string.Empty;
    });

    // Hiển thị URL truy cập
    Console.WriteLine("=======================================================");
    Console.WriteLine("Ứng dụng đang chạy tại:");
    Console.WriteLine($"- HTTP: http://localhost:5000");
    Console.WriteLine($"- HTTPS: https://localhost:5001");

    // Lấy địa chỉ IP của máy
    var hostName = Dns.GetHostName();
    var ips = Dns.GetHostAddresses(hostName)
        .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .Select(ip => ip.ToString());

    foreach (var ip in ips)
    {
        Console.WriteLine($"- HTTP: http://{ip}:5000");
        Console.WriteLine($"- HTTPS: https://{ip}:5001");
    }
    Console.WriteLine("=======================================================");

    // Tự động mở trình duyệt khi khởi động trong môi trường Development
    if (builder.Configuration.GetValue<bool>("OpenBrowserOnStartup", true))
    {
        string url = "http://localhost:5000";
        OpenBrowser(url);
    }
}

app.UseHttpsRedirection();

// Sử dụng CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Hàm mở trình duyệt web mặc định
static void OpenBrowser(string url)
{
    try
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Không thể mở trình duyệt: {ex.Message}");
    }
}
