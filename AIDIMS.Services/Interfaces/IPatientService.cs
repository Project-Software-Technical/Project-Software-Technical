using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IPatientService : ICoreService<Patient, CreatePatientRequest, UpdatePatientRequest, PatientResponse>
    {
        // Thêm các phương thức đặc biệt tại đây nếu cần
    }
}