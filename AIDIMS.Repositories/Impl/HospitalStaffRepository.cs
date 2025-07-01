using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class HospitalStaffRepository : GenericRepository<HospitalStaff>, IHospitalStaffRepository
    {
        public HospitalStaffRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}