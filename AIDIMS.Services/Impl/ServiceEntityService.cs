using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ServiceEntityService : IServiceEntityService
    {
        private readonly IServiceRepository _repository;
        private readonly IMapper _mapper;

        public ServiceEntityService(IServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<ServiceResponse> CreateAsync(CreateServiceRequest createRequest)
        {
            var service = _mapper.Map<Service>(createRequest);
            service.CreatedDate = DateTime.UtcNow;
            service.Status = service.Status ?? "Hoạt động";

            var result = await _repository.AddAsync(service);
            return _mapper.Map<ServiceResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<ServiceResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            return new PagedResponse<ServiceResponse>
            {
                Items = _mapper.Map<IEnumerable<ServiceResponse>>(entities),
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ServiceResponse>(entity);
        }

        public async Task<PagedResponse<ServiceResponse>> GetServicesByStatusAsync(string status, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo status trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo status
            var allServices = await _repository.GetAllAsync(pageNumber, pageSize);
            var filteredServices = allServices.Where(s => s.Status == status).ToList();

            // Thực hiện phân trang
            var pagedServices = filteredServices
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<ServiceResponse>
            {
                Items = _mapper.Map<IEnumerable<ServiceResponse>>(pagedServices),
                TotalCount = filteredServices.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ServiceResponse> UpdateAsync(int id, UpdateServiceRequest updateRequest)
        {
            // Lấy thông tin hiện tại của service
            var existingService = await _repository.GetByIdAsync(id);
            if (existingService == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var serviceToUpdate = _mapper.Map<Service>(updateRequest);
            serviceToUpdate.ServiceID = id;
            serviceToUpdate.CreatedDate = existingService.CreatedDate;

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, serviceToUpdate);
            return _mapper.Map<ServiceResponse>(result);
        }
    }
}