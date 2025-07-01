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
    public class DiagnosisResultService : IDiagnosisResultService
    {
        private readonly IDiagnosisResultRepository _repository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public DiagnosisResultService(
            IDiagnosisResultRepository repository,
            IMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _repository = repository;
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<DiagnosisResultResponse> CreateAsync(CreateDiagnosisResultRequest createRequest)
        {
            var diagnosisResult = _mapper.Map<DiagnosisResult>(createRequest);

            // Thiết lập giá trị mặc định nếu cần
            diagnosisResult.Status = diagnosisResult.Status ?? "Mới";
            if (diagnosisResult.DiagnosisDate == default)
            {
                diagnosisResult.DiagnosisDate = DateTime.UtcNow;
            }

            var result = await _repository.AddAsync(diagnosisResult);

            var response = _mapper.Map<DiagnosisResultResponse>(result);

            // Thêm thông tin liên quan
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(result.RecordID);
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }
            }

            return response;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<DiagnosisResultResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            var responses = _mapper.Map<IEnumerable<DiagnosisResultResponse>>(entities).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var medicalRecord = await _medicalRecordRepository.GetByIdAsync(response.RecordID);
                if (medicalRecord != null)
                {
                    var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                    if (patient != null)
                    {
                        response.PatientName = patient.FullName;
                    }
                }
            }

            return new PagedResponse<DiagnosisResultResponse>
            {
                Items = responses,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DiagnosisResultResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<DiagnosisResultResponse>(entity);

            // Thêm thông tin liên quan
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(entity.RecordID);
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }
            }

            return response;
        }

        public async Task<DiagnosisResultDetailResponse> GetDiagnosisResultDetailAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<DiagnosisResultDetailResponse>(entity);

            // Lấy thông tin hồ sơ bệnh án và bệnh nhân
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(entity.RecordID);
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }

                response.MedicalRecord = _mapper.Map<MedicalRecordResponse>(medicalRecord);
            }

            return response;
        }

        public async Task<PagedResponse<DiagnosisResultResponse>> GetDiagnosisResultsByRecordIdAsync(int recordId, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo recordId trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo recordId
            var allResults = await _repository.GetAllAsync(1, int.MaxValue);
            var filteredResults = allResults.Where(r => r.RecordID == recordId).ToList();

            // Thực hiện phân trang
            var pagedResults = filteredResults
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responses = _mapper.Map<IEnumerable<DiagnosisResultResponse>>(pagedResults).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(recordId);
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    foreach (var response in responses)
                    {
                        response.PatientName = patient.FullName;
                    }
                }
            }

            return new PagedResponse<DiagnosisResultResponse>
            {
                Items = responses,
                TotalCount = filteredResults.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DiagnosisResultResponse> UpdateAsync(int id, UpdateDiagnosisResultRequest updateRequest)
        {
            // Lấy thông tin hiện tại của diagnosis result
            var existingResult = await _repository.GetByIdAsync(id);
            if (existingResult == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var resultToUpdate = _mapper.Map<DiagnosisResult>(updateRequest);
            resultToUpdate.DiagnosisResultID = id;
            resultToUpdate.RecordID = existingResult.RecordID; // Giữ nguyên RecordID

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, resultToUpdate);

            var response = _mapper.Map<DiagnosisResultResponse>(result);

            // Thêm thông tin liên quan
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(result.RecordID);
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }
            }

            return response;
        }
    }
}