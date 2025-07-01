using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IDiagnosisResultService : ICoreService<DiagnosisResult, CreateDiagnosisResultRequest, UpdateDiagnosisResultRequest, DiagnosisResultResponse>
    {
        /// <summary>
        /// Lấy thông tin chi tiết của kết quả chẩn đoán theo ID
        /// </summary>
        /// <param name="id">ID của kết quả chẩn đoán</param>
        /// <returns>Thông tin chi tiết của kết quả chẩn đoán</returns>
        Task<DiagnosisResultDetailResponse> GetDiagnosisResultDetailAsync(int id);

        /// <summary>
        /// Lấy danh sách kết quả chẩn đoán theo mã hồ sơ bệnh án
        /// </summary>
        /// <param name="recordId">Mã hồ sơ bệnh án</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các kết quả chẩn đoán của hồ sơ bệnh án</returns>
        Task<PagedResponse<DiagnosisResultResponse>> GetDiagnosisResultsByRecordIdAsync(int recordId, int pageNumber, int pageSize);
    }
}