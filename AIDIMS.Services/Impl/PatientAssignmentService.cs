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
    public class PatientAssignmentService : IPatientAssignmentService
    {
        private readonly IPatientAssignmentRepository _repository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public PatientAssignmentService(IPatientAssignmentRepository repository,
            IPatientRepository patientRepository,
            IHospitalStaffRepository staffRepository,
            IMapper mapper)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync() => _repository.CountAsync();

        public async Task<PatientAssignmentResponse> CreateAsync(CreatePatientAssignmentRequest createRequest)
        {
            var entity = _mapper.Map<PatientAssignment>(createRequest);
            var saved = await _repository.AddAsync(entity);
            return await ConvertToResponseAsync(saved);
        }

        public async Task<bool> DeleteByIdAsync(int id) => await _repository.DeleteByIdAsync(id);

        public async Task<PagedResponse<PatientAssignmentResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var items = await _repository.GetAllAsync(pageNumber, pageSize);
            var list = new List<PatientAssignmentResponse>();
            foreach (var item in items)
            {
                list.Add(await ConvertToResponseAsync(item));
            }
            return new PagedResponse<PatientAssignmentResponse>
            {
                Items = list,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PatientAssignmentResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return await ConvertToResponseAsync(entity);
        }

        public async Task<PatientAssignmentResponse> UpdateAsync(int id, CreatePatientAssignmentRequest updateRequest)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            var toUpdate = _mapper.Map<PatientAssignment>(updateRequest);
            toUpdate.AssignmentID = id;

            var result = await _repository.UpdateAsync(id, toUpdate);
            return await ConvertToResponseAsync(result);
        }

        public async Task<PagedResponse<PatientAssignmentResponse>> GetAssignmentsByDoctorAsync(int doctorId, int pageNumber, int pageSize)
        {
            var all = await _repository.GetAllAsync(1, int.MaxValue);
            var filtered = all.Where(p => p.DoctorID == doctorId)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize);
            var list = new List<PatientAssignmentResponse>();
            foreach (var item in filtered)
            {
                list.Add(await ConvertToResponseAsync(item));
            }
            return new PagedResponse<PatientAssignmentResponse>
            {
                Items = list,
                TotalCount = list.Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private async Task<PatientAssignmentResponse> ConvertToResponseAsync(PatientAssignment entity)
        {
            if (entity == null) return null;
            var resp = _mapper.Map<PatientAssignmentResponse>(entity);
            var patient = await _patientRepository.GetByIdAsync(entity.PatientID);
            if (patient != null) resp.PatientName = patient.FullName;
            var doctor = await _staffRepository.GetByIdAsync(entity.DoctorID);
            if (doctor != null) resp.DoctorName = doctor.FullName;
            resp.AssignedDate = entity.AssignedDate.ToString("yyyy-MM-dd HH:mm:ss");
            return resp;
        }
    }
}