using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IUserService : ICoreService<User>
    {
        // Kế thừa từ ICoreService, không cần thêm phương thức khác
    }
}