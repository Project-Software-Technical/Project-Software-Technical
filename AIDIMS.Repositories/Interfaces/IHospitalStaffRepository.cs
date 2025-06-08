using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IHospitalStaffRepository : IGenericRepository<HospitalStaff>
    {
        // Kế thừa từ IGenericRepository, không cần thêm phương thức khác
    }
}