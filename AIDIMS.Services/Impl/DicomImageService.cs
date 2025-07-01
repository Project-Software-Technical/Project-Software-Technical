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
    public class DicomImageService : IDicomImageService
    {
        private readonly IDicomImageRepository _repository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IHospitalStaffRepository _hospitalStaffRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public DicomImageService(
            IDicomImageRepository repository,
            IMedicalRecordRepository medicalRecordRepository,
            IHospitalStaffRepository hospitalStaffRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _repository = repository;
            _medicalRecordRepository = medicalRecordRepository;
            _hospitalStaffRepository = hospitalStaffRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<DicomImageResponse> CreateAsync(CreateDicomImageRequest createRequest)
        {
            var dicomImage = _mapper.Map<DicomImage>(createRequest);

            // Thiết lập giá trị mặc định nếu cần
            dicomImage.CreatedDate = DateTime.UtcNow;

            var result = await _repository.AddAsync(dicomImage);

            var response = _mapper.Map<DicomImageResponse>(result);

            // Thêm thông tin liên quan
            if (result.TechnicianID.HasValue)
            {
                var technician = await _hospitalStaffRepository.GetByIdAsync(result.TechnicianID.Value);
                if (technician != null)
                {
                    response.TechnicianName = technician.FullName;
                }
            }

            // Lấy thông tin bệnh nhân qua hồ sơ bệnh án
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

        public async Task<PagedResponse<DicomImageResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            var responses = _mapper.Map<IEnumerable<DicomImageResponse>>(entities).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                if (response.TechnicianID.HasValue)
                {
                    var technician = await _hospitalStaffRepository.GetByIdAsync(response.TechnicianID.Value);
                    if (technician != null)
                    {
                        response.TechnicianName = technician.FullName;
                    }
                }

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

            return new PagedResponse<DicomImageResponse>
            {
                Items = responses,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DicomImageResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<DicomImageResponse>(entity);

            // Thêm thông tin liên quan
            if (response.TechnicianID.HasValue)
            {
                var technician = await _hospitalStaffRepository.GetByIdAsync(entity.TechnicianID.Value);
                if (technician != null)
                {
                    response.TechnicianName = technician.FullName;
                }
            }

            // Lấy thông tin bệnh nhân qua hồ sơ bệnh án
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

        public async Task<DicomImageDetailResponse> GetDicomImageDetailAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<DicomImageDetailResponse>(entity);

            // Lấy thông tin kỹ thuật viên
            if (entity.TechnicianID.HasValue)
            {
                var technician = await _hospitalStaffRepository.GetByIdAsync(entity.TechnicianID.Value);
                if (technician != null)
                {
                    response.TechnicianName = technician.FullName;
                    response.Technician = _mapper.Map<HospitalStaffResponse>(technician);
                }
            }

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

        public async Task<PagedResponse<DicomImageResponse>> GetDicomImagesByRecordIdAsync(int recordId, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo recordId trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo recordId
            var allImages = await _repository.GetAllAsync(1, int.MaxValue);
            var filteredImages = allImages.Where(i => i.RecordID == recordId).ToList();

            // Thực hiện phân trang
            var pagedImages = filteredImages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responses = _mapper.Map<IEnumerable<DicomImageResponse>>(pagedImages).ToList();

            // Lấy thông tin bệnh nhân từ hồ sơ bệnh án
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(recordId);
            string patientName = null;
            if (medicalRecord != null)
            {
                var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                if (patient != null)
                {
                    patientName = patient.FullName;
                }
            }

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                response.PatientName = patientName;

                if (response.TechnicianID.HasValue)
                {
                    var technician = await _hospitalStaffRepository.GetByIdAsync(response.TechnicianID.Value);
                    if (technician != null)
                    {
                        response.TechnicianName = technician.FullName;
                    }
                }
            }

            return new PagedResponse<DicomImageResponse>
            {
                Items = responses,
                TotalCount = filteredImages.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DicomImageResponse> ApproveDicomImageAsync(int id, bool isApproved, string doctorNotes)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.IsApproved = isApproved;
            entity.DoctorNotes = doctorNotes;

            var result = await _repository.UpdateAsync(id, entity);

            return await GetByIdAsync(id);
        }

        public async Task<DicomImageResponse> UpdateAIFeedbackAsync(int id, string aiFeedback)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.AIFeedback = aiFeedback;

            var result = await _repository.UpdateAsync(id, entity);

            return await GetByIdAsync(id);
        }

        public async Task<DicomImageResponse> UpdateAsync(int id, UpdateDicomImageRequest updateRequest)
        {
            // Lấy thông tin hiện tại của dicom image
            var existingImage = await _repository.GetByIdAsync(id);
            if (existingImage == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            _mapper.Map(updateRequest, existingImage);
            existingImage.ImageID = id;

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, existingImage);

            return await GetByIdAsync(id);
        }
    }
}