using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Interfaces;
using AutoMapper;

namespace AIDIMS.Services.Impl
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync() => _repository.CountAsync();

        public async Task<DepartmentResponse> CreateAsync(CreateDepartmentRequest createRequest)
        {
            var entity = _mapper.Map<Department>(createRequest);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<DepartmentResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id) => await _repository.DeleteByIdAsync(id);

        public async Task<PagedResponse<DepartmentResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            var mapped = _mapper.Map<IEnumerable<DepartmentResponse>>(entities);
            return new PagedResponse<DepartmentResponse>
            {
                Items = mapped,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DepartmentResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DepartmentResponse>(entity);
        }

        public async Task<DepartmentResponse> UpdateAsync(int id, UpdateDepartmentRequest updateRequest)
        {
            var entityToUpdate = _mapper.Map<Department>(updateRequest);
            entityToUpdate.DepartmentID = id;
            var updated = await _repository.UpdateAsync(id, entityToUpdate);
            return _mapper.Map<DepartmentResponse>(updated);
        }
    }
}