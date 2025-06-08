using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using System.Threading.Tasks;

namespace AIDIMS.Services.Interfaces
{
    public interface IRoleService : ICoreService<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>
    {

    }
}