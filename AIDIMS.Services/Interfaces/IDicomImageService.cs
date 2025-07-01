using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IDicomImageService : ICoreService<DicomImage, CreateDicomImageRequest, UpdateDicomImageRequest, DicomImageResponse>
    {
        /// <summary>
        /// Lấy thông tin chi tiết của hình ảnh DICOM theo ID
        /// </summary>
        /// <param name="id">ID của hình ảnh DICOM</param>
        /// <returns>Thông tin chi tiết của hình ảnh DICOM</returns>
        Task<DicomImageDetailResponse> GetDicomImageDetailAsync(int id);

        /// <summary>
        /// Lấy danh sách hình ảnh DICOM theo mã hồ sơ bệnh án
        /// </summary>
        /// <param name="recordId">Mã hồ sơ bệnh án</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các hình ảnh DICOM của hồ sơ bệnh án</returns>
        Task<PagedResponse<DicomImageResponse>> GetDicomImagesByRecordIdAsync(int recordId, int pageNumber, int pageSize);

        /// <summary>
        /// Phê duyệt hoặc từ chối hình ảnh DICOM
        /// </summary>
        /// <param name="id">ID của hình ảnh DICOM</param>
        /// <param name="isApproved">Trạng thái phê duyệt</param>
        /// <param name="doctorNotes">Ghi chú của bác sĩ</param>
        /// <returns>Kết quả của thao tác phê duyệt</returns>
        Task<DicomImageResponse> ApproveDicomImageAsync(int id, bool isApproved, string doctorNotes);

        /// <summary>
        /// Cập nhật phản hồi AI cho hình ảnh DICOM
        /// </summary>
        /// <param name="id">ID của hình ảnh DICOM</param>
        /// <param name="aiFeedback">Phản hồi AI</param>
        /// <returns>Kết quả của thao tác cập nhật phản hồi AI</returns>
        Task<DicomImageResponse> UpdateAIFeedbackAsync(int id, string aiFeedback);
    }
}