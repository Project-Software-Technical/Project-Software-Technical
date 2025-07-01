using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Repositories.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.Role)
                .Include(u => u.HospitalStaff)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByStaffIdAsync(int staffId)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.Role)
                .Include(u => u.HospitalStaff)
                .FirstOrDefaultAsync(u => u.StaffID == staffId);
        }
    }
}