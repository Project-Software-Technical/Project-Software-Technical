using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Repositories.Impl
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}