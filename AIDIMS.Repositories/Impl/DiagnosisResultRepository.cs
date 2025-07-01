using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class DiagnosisResultRepository : GenericRepository<DiagnosisResult>, IDiagnosisResultRepository
    {
        public DiagnosisResultRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}