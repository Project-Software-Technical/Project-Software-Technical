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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository repository, IPatientRepository patientRepository, IMapper mapper)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync() => _repository.CountAsync();

        public async Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest createRequest)
        {
            var appointment = _mapper.Map<Appointment>(createRequest);
            appointment.Status ??= "New";

            var result = await _repository.AddAsync(appointment);
            var response = _mapper.Map<AppointmentResponse>(result);

            var patient = await _patientRepository.GetByIdAsync(result.PatientID);
            if (patient != null) response.PatientName = patient.FullName;
            return response;
        }

        public async Task<bool> DeleteByIdAsync(int id) => await _repository.DeleteByIdAsync(id);

        public async Task<PagedResponse<AppointmentResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var items = await _repository.GetAllAsync(pageNumber, pageSize);
            var mapped = _mapper.Map<IEnumerable<AppointmentResponse>>(items);
            return new PagedResponse<AppointmentResponse>
            {
                Items = mapped,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var resp = _mapper.Map<AppointmentResponse>(entity);
            if (entity != null)
            {
                var patient = await _patientRepository.GetByIdAsync(entity.PatientID);
                if (patient != null) resp.PatientName = patient.FullName;
            }
            return resp;
        }

        public async Task<AppointmentResponse> UpdateAsync(int id, UpdateAppointmentRequest updateRequest)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            var toUpdate = _mapper.Map<Appointment>(updateRequest);
            toUpdate.AppointmentID = id;

            var result = await _repository.UpdateAsync(id, toUpdate);
            var resp = _mapper.Map<AppointmentResponse>(result);
            var patient = await _patientRepository.GetByIdAsync(result.PatientID);
            if (patient != null) resp.PatientName = patient.FullName;
            return resp;
        }

        public async Task<AppointmentResponse> UpdateStatusAsync(int id, UpdateAppointmentStatusRequest statusRequest)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(statusRequest.Status))
            {
                existing.Status = statusRequest.Status;
                await _repository.SaveChangesAsync();
            }

            var resp = _mapper.Map<AppointmentResponse>(existing);
            var patient = await _patientRepository.GetByIdAsync(existing.PatientID);
            if (patient != null) resp.PatientName = patient.FullName;
            return resp;
        }
    }
}