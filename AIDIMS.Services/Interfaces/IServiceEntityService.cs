using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;
using System.Threading.Tasks;

namespace AIDIMS.Services.Interfaces
{
    public interface IServiceEntityService : ICoreService<Service, CreateServiceRequest, UpdateServiceRequest, ServiceResponse>
    {
        /// <summary>
        /// Lấy danh sách dịch vụ theo trạng thái
        /// </summary>
        /// <param name="status">Trạng thái dịch vụ</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách dịch vụ theo trạng thái</returns>
        Task<PagedResponse<ServiceResponse>> GetServicesByStatusAsync(string status, int pageNumber, int pageSize);
    }
}