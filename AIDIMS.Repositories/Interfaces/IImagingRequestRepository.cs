using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IImagingRequestRepository : IGenericRepository<ImagingRequest>
    {
        // Kế thừa từ IGenericRepository, không cần thêm phương thức khác
    }
}