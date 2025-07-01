using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IMedicalRecordService : ICoreService<MedicalRecord, CreateMedicalRecordRequest, UpdateMedicalRecordRequest, MedicalRecordResponse>
    {
        /// <summary>
        /// Lấy thông tin chi tiết của hồ sơ bệnh án theo ID
        /// </summary>
        /// <param name="id">ID của hồ sơ bệnh án</param>
        /// <returns>Thông tin chi tiết của hồ sơ bệnh án</returns>
        Task<MedicalRecordDetailResponse> GetMedicalRecordDetailAsync(int id);

        /// <summary>
        /// Lấy danh sách hồ sơ bệnh án theo ID của bệnh nhân
        /// </summary>
        /// <param name="patientId">ID của bệnh nhân</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các hồ sơ bệnh án của bệnh nhân</returns>
        Task<PagedResponse<MedicalRecordResponse>> GetMedicalRecordsByPatientIdAsync(int patientId, int pageNumber, int pageSize);
    }
}