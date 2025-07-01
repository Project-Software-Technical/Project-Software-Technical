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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<PatientResponse> CreateAsync(CreatePatientRequest createRequest)
        {
            var patient = _mapper.Map<Patient>(createRequest);
            patient.CreatedDate = DateTime.UtcNow;

            var result = await _repository.AddAsync(patient);
            return _mapper.Map<PatientResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<PatientResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            return new PagedResponse<PatientResponse>
            {
                Items = _mapper.Map<IEnumerable<PatientResponse>>(entities),
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PatientResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<PatientResponse>(entity);
        }

        public async Task<PatientResponse> UpdateAsync(int id, UpdatePatientRequest updateRequest)
        {
            // Lấy thông tin hiện tại của bệnh nhân
            var existingPatient = await _repository.GetByIdAsync(id);
            if (existingPatient == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var patientToUpdate = _mapper.Map<Patient>(updateRequest);
            patientToUpdate.PatientID = id;
            patientToUpdate.CreatedDate = existingPatient.CreatedDate;

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, patientToUpdate);
            return _mapper.Map<PatientResponse>(result);
        }
    }
}