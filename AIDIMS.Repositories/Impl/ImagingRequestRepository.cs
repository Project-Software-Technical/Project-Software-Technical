using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class ImagingRequestRepository : GenericRepository<ImagingRequest>, IImagingRequestRepository
    {
        public ImagingRequestRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}