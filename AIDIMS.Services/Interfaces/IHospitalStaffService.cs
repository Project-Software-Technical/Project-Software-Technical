using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IHospitalStaffService : ICoreService<HospitalStaff, CreateHospitalStaffRequest, UpdateHospitalStaffRequest, HospitalStaffResponse>
    {
        // Thêm các phương thức đặc biệt tại đây nếu cần
    }
}