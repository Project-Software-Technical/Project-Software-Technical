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
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Services.Impl
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHospitalStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public MedicalRecordService(
            IMedicalRecordRepository repository,
            IPatientRepository patientRepository,
            IHospitalStaffRepository staffRepository,
            IMapper mapper)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<MedicalRecordResponse> CreateAsync(CreateMedicalRecordRequest createRequest)
        {
            // Kiểm tra xem lịch hẹn này đã có hồ sơ y tế hay chưa
            var duplicates = await _repository.GetAllAsync(1, int.MaxValue);
            var existed = duplicates.Cast<MedicalRecord>().FirstOrDefault(r => r.AppointmentID == createRequest.AppointmentID);
            if (existed != null)
            {
                // Trả về hồ sơ đã tồn tại để FE có thể dùng luôn
                return _mapper.Map<MedicalRecordResponse>(existed);
            }

            var medicalRecord = _mapper.Map<MedicalRecord>(createRequest);
            // Nếu client chưa truyền CreatedBy, tạm gán bằng DoctorID (hoặc 0 nếu null) để tránh lỗi ràng buộc
            if (medicalRecord.CreatedBy == 0)
            {
                medicalRecord.CreatedBy = medicalRecord.DoctorID ?? 0;
            }
            medicalRecord.CreatedDate = DateTime.UtcNow;
            medicalRecord.Status = medicalRecord.Status; // giữ nguyên RecordStatus đã set hoặc mặc định

            // Đảm bảo các cột NOT NULL trong DB không bị null
            medicalRecord.InitialDiagnosis ??= createRequest.Diagnosis ?? string.Empty;
            medicalRecord.FinalDiagnosis ??= string.Empty;
            medicalRecord.Treatment ??= string.Empty;
            medicalRecord.Prescription ??= string.Empty;

            var result = await _repository.AddAsync(medicalRecord);

            var response = _mapper.Map<MedicalRecordResponse>(result);

            // Thêm thông tin liên quan
            var patient = await _patientRepository.GetByIdAsync(result.PatientID);
            if (patient != null)
            {
                response.PatientName = patient.FullName;
            }

            var doctor = await _staffRepository.GetByIdAsync(result.DoctorID ?? 0);
            if (doctor != null)
            {
                response.DoctorName = doctor.FullName;
            }

            return response;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<MedicalRecordResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            var responses = _mapper.Map<IEnumerable<MedicalRecordResponse>>(entities).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var patient = await _patientRepository.GetByIdAsync(response.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }

                var doctor = await _staffRepository.GetByIdAsync(response.DoctorID);
                if (doctor != null)
                {
                    response.DoctorName = doctor.FullName;
                }
            }

            return new PagedResponse<MedicalRecordResponse>
            {
                Items = responses,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<MedicalRecordResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<MedicalRecordResponse>(entity);

            // Thêm thông tin liên quan
            var patient = await _patientRepository.GetByIdAsync(entity.PatientID);
            if (patient != null)
            {
                response.PatientName = patient.FullName;
            }

            var doctor = await _staffRepository.GetByIdAsync(entity.DoctorID ?? 0);
            if (doctor != null)
            {
                response.DoctorName = doctor.FullName;
            }

            return response;
        }

        public async Task<MedicalRecordDetailResponse> GetMedicalRecordDetailAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<MedicalRecordDetailResponse>(entity);

            // Lấy thông tin bệnh nhân
            var patient = await _patientRepository.GetByIdAsync(entity.PatientID);
            if (patient != null)
            {
                response.PatientName = patient.FullName;
                response.Patient = _mapper.Map<PatientResponse>(patient);
            }

            // Lấy thông tin bác sĩ
            var doctor = await _staffRepository.GetByIdAsync(entity.DoctorID ?? 0);
            if (doctor != null)
            {
                response.DoctorName = doctor.FullName;
                response.Doctor = _mapper.Map<HospitalStaffResponse>(doctor);
            }

            // TODO: Lấy các thông tin liên quan khác như ImagingRequests, DiagnosisResults, DicomImages
            // Để tổng hợp đầy đủ, cần truy vấn và map các thông tin từ các entity khác

            return response;
        }

        public async Task<PagedResponse<MedicalRecordResponse>> GetMedicalRecordsByPatientIdAsync(int patientId, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo patientId trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo patientId
            var allRecords = await _repository.GetAllAsync(pageNumber, pageSize);
            var filteredRecords = allRecords.Where(r => r.PatientID == patientId).ToList();

            // Thực hiện phân trang
            var pagedRecords = filteredRecords
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responses = _mapper.Map<IEnumerable<MedicalRecordResponse>>(pagedRecords).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var patient = await _patientRepository.GetByIdAsync(response.PatientID);
                if (patient != null)
                {
                    response.PatientName = patient.FullName;
                }

                var doctor = await _staffRepository.GetByIdAsync(response.DoctorID);
                if (doctor != null)
                {
                    response.DoctorName = doctor.FullName;
                }
            }

            return new PagedResponse<MedicalRecordResponse>
            {
                Items = responses,
                TotalCount = filteredRecords.Count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<MedicalRecordResponse> UpdateAsync(int id, UpdateMedicalRecordRequest updateRequest)
        {
            // Lấy thông tin hiện tại của medical record
            var existingRecord = await _repository.GetByIdAsync(id);
            if (existingRecord == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var recordToUpdate = _mapper.Map<MedicalRecord>(updateRequest);
            recordToUpdate.RecordID = id;
            recordToUpdate.PatientID = existingRecord.PatientID; // Giữ nguyên PatientID
            recordToUpdate.CreatedDate = existingRecord.CreatedDate;

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, recordToUpdate);

            var response = _mapper.Map<MedicalRecordResponse>(result);

            // Thêm thông tin liên quan
            var patient = await _patientRepository.GetByIdAsync(result.PatientID);
            if (patient != null)
            {
                response.PatientName = patient.FullName;
            }

            var doctor = await _staffRepository.GetByIdAsync(result.DoctorID ?? 0);
            if (doctor != null)
            {
                response.DoctorName = doctor.FullName;
            }

            return response;
        }
    }
}