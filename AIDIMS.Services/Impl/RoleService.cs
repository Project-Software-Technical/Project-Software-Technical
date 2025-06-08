using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.Models;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Interfaces;
using AutoMapper;
using AIDIMS.Core.Interfaces;

namespace AIDIMS.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<RoleResponse> CreateAsync(CreateRoleRequest createRequest)
        {
            var role = _mapper.Map<Role>(createRequest);
            var result = await _repository.AddAsync(role);
            return _mapper.Map<RoleResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<RoleResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            return new PagedResponse<RoleResponse>
            {
                Items = _mapper.Map<IEnumerable<RoleResponse>>(entities),
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<RoleResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<RoleResponse>(entity);
        }

        public async Task<RoleResponse> UpdateAsync(int id, UpdateRoleRequest updateRequest)
        {
            var role = _mapper.Map<Role>(updateRequest);
            var result = await _repository.UpdateAsync(id, role);
            return _mapper.Map<RoleResponse>(result);
        }
    }
}