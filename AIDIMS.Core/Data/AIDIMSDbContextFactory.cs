using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AIDIMS.Core.Data
{
    public class AIDIMSDbContextFactory : IDesignTimeDbContextFactory<AIDIMSDbContext>
    {
        public AIDIMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AIDIMSDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=aidims_db;Username=postgres;Password=1234");

            return new AIDIMSDbContext(optionsBuilder.Options);
        }
    }
}