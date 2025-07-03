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
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình để lắng nghe trên tất cả các địa chỉ IP
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5000); // HTTP
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
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
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDiagnosisResultRepository, DiagnosisResultRepository>();
builder.Services.AddScoped<IDicomImageRepository, DicomImageRepository>();
builder.Services.AddScoped<IHospitalStaffRepository, HospitalStaffRepository>();
builder.Services.AddScoped<IImagingRequestRepository, ImagingRequestRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPatientAssignmentRepository, PatientAssignmentRepository>();

// Đăng ký Services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IHospitalStaffService, HospitalStaffService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IServiceEntityService, ServiceEntityService>();
builder.Services.AddScoped<IImagingRequestService, ImagingRequestService>();
builder.Services.AddScoped<IDiagnosisResultService, DiagnosisResultService>();
builder.Services.AddScoped<IDicomImageService, DicomImageService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPatientAssignmentService, PatientAssignmentService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

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

// Sử dụng CORS (đặt trước khi redirect để preflight không bị chặn)
app.UseCors("AllowAll");

app.UseHttpsRedirection();

// Cấu hình StaticFiles để phục vụ cả file DICOM (.dcm)
var staticProvider = new FileExtensionContentTypeProvider();
staticProvider.Mappings[".dcm"] = "application/dicom";
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream",
    ContentTypeProvider = staticProvider,
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot"))
});

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
