using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IImagingRequestService : ICoreService<ImagingRequest, CreateImagingRequestRequest, UpdateImagingRequestRequest, ImagingRequestResponse>
    {
        /// <summary>
        /// Lấy thông tin chi tiết của yêu cầu chụp theo ID
        /// </summary>
        /// <param name="id">ID của yêu cầu chụp</param>
        /// <returns>Thông tin chi tiết của yêu cầu chụp</returns>
        Task<ImagingRequestDetailResponse> GetImagingRequestDetailAsync(int id);

        /// <summary>
        /// Lấy danh sách yêu cầu chụp theo mã hồ sơ bệnh án
        /// </summary>
        /// <param name="recordId">Mã hồ sơ bệnh án</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các yêu cầu chụp của hồ sơ bệnh án</returns>
        Task<PagedResponse<ImagingRequestResponse>> GetImagingRequestsByRecordIdAsync(int recordId, int pageNumber, int pageSize);

        /// <summary>
        /// Lấy danh sách yêu cầu chụp theo trạng thái
        /// </summary>
        /// <param name="status">Trạng thái</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các yêu cầu chụp theo trạng thái</returns>
        Task<PagedResponse<ImagingRequestResponse>> GetImagingRequestsByStatusAsync(string status, int pageNumber, int pageSize);
    }
}