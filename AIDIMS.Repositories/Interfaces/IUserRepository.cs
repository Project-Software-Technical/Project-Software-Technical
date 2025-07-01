using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IUserRepository : ICoreRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByStaffIdAsync(int staffId);
    }
}