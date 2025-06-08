using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // Kế thừa từ IGenericRepository, không cần thêm phương thức khác
    }
}