using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}