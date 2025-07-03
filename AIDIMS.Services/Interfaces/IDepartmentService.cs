using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IDepartmentService : ICoreService<Department, CreateDepartmentRequest, UpdateDepartmentRequest, DepartmentResponse>
    {
    }
}