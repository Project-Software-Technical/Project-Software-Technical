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
    public class ImagingRequestService : IImagingRequestService
    {
        private readonly IImagingRequestRepository _repository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public ImagingRequestService(
            IImagingRequestRepository repository,
            IMedicalRecordRepository medicalRecordRepository,
            IServiceRepository serviceRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _repository = repository;
            _medicalRecordRepository = medicalRecordRepository;
            _serviceRepository = serviceRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<ImagingRequestResponse> CreateAsync(CreateImagingRequestRequest createRequest)
        {
            var imagingRequest = _mapper.Map<ImagingRequest>(createRequest);

            // Thiết lập giá trị mặc định nếu cần
            imagingRequest.Status = imagingRequest.Status ?? "Mới";
            if (imagingRequest.RequestDate == default)
            {
                imagingRequest.RequestDate = DateTime.UtcNow;
            }

            var result = await _repository.AddAsync(imagingRequest);

            var response = _mapper.Map<ImagingRequestResponse>(result);

            // Thêm thông tin liên quan
            var service = await _serviceRepository.GetByIdAsync(result.ServiceID);
            if (service != null)
            {
                response.ServiceName = service.ServiceName;
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

        public async Task<PagedResponse<ImagingRequestResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            var responses = _mapper.Map<IEnumerable<ImagingRequestResponse>>(entities).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var service = await _serviceRepository.GetByIdAsync(response.ServiceID);
                if (service != null)
                {
                    response.ServiceName = service.ServiceName;
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

            return new PagedResponse<ImagingRequestResponse>
            {
                Items = responses,
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ImagingRequestResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<ImagingRequestResponse>(entity);

            // Thêm thông tin liên quan
            var service = await _serviceRepository.GetByIdAsync(entity.ServiceID);
            if (service != null)
            {
                response.ServiceName = service.ServiceName;
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

        public async Task<ImagingRequestDetailResponse> GetImagingRequestDetailAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var response = _mapper.Map<ImagingRequestDetailResponse>(entity);

            // Lấy thông tin dịch vụ
            var service = await _serviceRepository.GetByIdAsync(entity.ServiceID);
            if (service != null)
            {
                response.ServiceName = service.ServiceName;
                response.Service = _mapper.Map<ServiceResponse>(service);
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

            // TODO: Lấy danh sách hình ảnh DICOM liên quan
            // Cần có repository cho DICOM Images

            return response;
        }

        public async Task<PagedResponse<ImagingRequestResponse>> GetImagingRequestsByRecordIdAsync(int recordId, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo recordId trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo recordId
            var allRequests = await _repository.GetAllAsync(1, int.MaxValue);
            var filteredRequests = allRequests.Where(r => r.RecordID == recordId).ToList();

            // Thực hiện phân trang
            var pagedRequests = filteredRequests
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responses = _mapper.Map<IEnumerable<ImagingRequestResponse>>(pagedRequests).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var service = await _serviceRepository.GetByIdAsync(response.ServiceID);
                if (service != null)
                {
                    response.ServiceName = service.ServiceName;
                }

                var medicalRecord = await _medicalRecordRepository.GetByIdAsync(recordId);
                if (medicalRecord != null)
                {
                    var patient = await _patientRepository.GetByIdAsync(medicalRecord.PatientID);
                    if (patient != null)
                    {
                        response.PatientName = patient.FullName;
                    }
                }
            }

            return new PagedResponse<ImagingRequestResponse>
            {
                Items = responses,
                TotalCount = filteredRequests.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResponse<ImagingRequestResponse>> GetImagingRequestsByStatusAsync(string status, int pageNumber, int pageSize)
        {
            // TODO: Cần cài đặt phương thức truy vấn theo status trong repository
            // Hiện tại sẽ dùng cách đơn giản, lấy tất cả và filter theo status
            var allRequests = await _repository.GetAllAsync(1, int.MaxValue);
            var filteredRequests = allRequests.Where(r => r.Status == status).ToList();

            // Thực hiện phân trang
            var pagedRequests = filteredRequests
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responses = _mapper.Map<IEnumerable<ImagingRequestResponse>>(pagedRequests).ToList();

            // Thêm thông tin liên quan cho mỗi bản ghi
            foreach (var response in responses)
            {
                var service = await _serviceRepository.GetByIdAsync(response.ServiceID);
                if (service != null)
                {
                    response.ServiceName = service.ServiceName;
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

            return new PagedResponse<ImagingRequestResponse>
            {
                Items = responses,
                TotalCount = filteredRequests.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ImagingRequestResponse> UpdateAsync(int id, UpdateImagingRequestRequest updateRequest)
        {
            // Lấy thông tin hiện tại của imaging request
            var existingRequest = await _repository.GetByIdAsync(id);
            if (existingRequest == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var requestToUpdate = _mapper.Map<ImagingRequest>(updateRequest);
            requestToUpdate.RequestID = id;
            requestToUpdate.RecordID = existingRequest.RecordID; // Giữ nguyên RecordID
            requestToUpdate.RequestDate = existingRequest.RequestDate; // Giữ nguyên RequestDate

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, requestToUpdate);

            var response = _mapper.Map<ImagingRequestResponse>(result);

            // Thêm thông tin liên quan
            var service = await _serviceRepository.GetByIdAsync(result.ServiceID);
            if (service != null)
            {
                response.ServiceName = service.ServiceName;
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
    }
}