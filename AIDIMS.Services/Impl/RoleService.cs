using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Interfaces;

namespace AIDIMS.Services.Impl
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}