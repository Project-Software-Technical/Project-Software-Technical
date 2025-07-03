using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class PatientAssignmentRepository : GenericRepository<PatientAssignment>, IPatientAssignmentRepository
    {
        public PatientAssignmentRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}