using AutoMapper;
using AIDIMS.Core.Models;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;

namespace AIDIMS.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Role mappings
            CreateMap<Role, RoleResponse>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<UpdateRoleRequest, Role>();
        }
    }
}