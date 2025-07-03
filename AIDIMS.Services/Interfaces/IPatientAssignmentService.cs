using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IPatientAssignmentService : ICoreService<PatientAssignment, CreatePatientAssignmentRequest, CreatePatientAssignmentRequest, PatientAssignmentResponse>
    {
        Task<PagedResponse<PatientAssignmentResponse>> GetAssignmentsByDoctorAsync(int doctorId, int pageNumber, int pageSize);
    }
}