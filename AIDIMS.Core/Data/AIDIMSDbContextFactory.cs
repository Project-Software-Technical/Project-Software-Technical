using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AIDIMS.Core.Data
{
    public class AIDIMSDbContextFactory : IDesignTimeDbContextFactory<AIDIMSDbContext>
    {
        public AIDIMSDbContext CreateDbContext(string[] args)
        {
            // Đọc cấu hình từ appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AIDIMSDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Nếu không tìm thấy chuỗi kết nối trong file appsettings.json, sử dụng chuỗi kết nối mặc định
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Host=localhost;Port=5432;Database=aidims_db;Username=postgres;Password=1234";
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new AIDIMSDbContext(optionsBuilder.Options);
        }
    }
}