using System;
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
    public class HospitalStaffService : IHospitalStaffService
    {
        private readonly IHospitalStaffRepository _repository;
        private readonly IMapper _mapper;

        public HospitalStaffService(IHospitalStaffRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<HospitalStaffResponse> CreateAsync(CreateHospitalStaffRequest createRequest)
        {
            var hospitalStaff = _mapper.Map<HospitalStaff>(createRequest);
            // Bảo đảm BirthDate có Kind = Utc để tránh lỗi Npgsql
            hospitalStaff.BirthDate = DateTime.SpecifyKind(hospitalStaff.BirthDate, DateTimeKind.Utc);
            hospitalStaff.CreatedDate = DateTime.UtcNow;
            hospitalStaff.Status = hospitalStaff.Status ?? "Đang làm việc";

            var result = await _repository.AddAsync(hospitalStaff);
            return _mapper.Map<HospitalStaffResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<HospitalStaffResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            return new PagedResponse<HospitalStaffResponse>
            {
                Items = _mapper.Map<IEnumerable<HospitalStaffResponse>>(entities),
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<HospitalStaffResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<HospitalStaffResponse>(entity);
        }

        public async Task<HospitalStaffResponse> UpdateAsync(int id, UpdateHospitalStaffRequest updateRequest)
        {
            // Lấy thông tin hiện tại của nhân viên
            var existingStaff = await _repository.GetByIdAsync(id);
            if (existingStaff == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var staffToUpdate = _mapper.Map<HospitalStaff>(updateRequest);
            staffToUpdate.StaffID = id;
            staffToUpdate.CreatedDate = existingStaff.CreatedDate;
            staffToUpdate.BirthDate = DateTime.SpecifyKind(staffToUpdate.BirthDate, DateTimeKind.Utc);

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, staffToUpdate);
            return _mapper.Map<HospitalStaffResponse>(result);
        }
    }
}