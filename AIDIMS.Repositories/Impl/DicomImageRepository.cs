using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class DicomImageRepository : GenericRepository<DicomImage>, IDicomImageRepository
    {
        public DicomImageRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}