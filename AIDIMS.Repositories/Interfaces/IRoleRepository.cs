using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        // Kế thừa từ IGenericRepository, không cần thêm phương thức khác
    }
}