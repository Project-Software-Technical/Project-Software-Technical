using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;

namespace AIDIMS.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<PagedResponse<DoctorResponse>> GetAllDoctorsAsync(int pageNumber, int pageSize);
        Task<PagedResponse<DoctorResponse>> GetDoctorsByDepartmentAsync(string departmentName, int pageNumber, int pageSize);
    }
}