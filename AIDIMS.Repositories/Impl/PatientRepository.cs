using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}