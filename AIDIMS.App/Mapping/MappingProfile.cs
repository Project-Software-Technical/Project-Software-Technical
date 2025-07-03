using AutoMapper;
using AIDIMS.Core.Models;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;

namespace AIDIMS.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Role mappings
            CreateMap<Role, RoleResponse>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<UpdateRoleRequest, Role>();

            // Patient mappings
            CreateMap<Patient, PatientResponse>();
            CreateMap<CreatePatientRequest, Patient>();
            CreateMap<UpdatePatientRequest, Patient>();

            // HospitalStaff mappings
            CreateMap<HospitalStaff, HospitalStaffResponse>();
            CreateMap<CreateHospitalStaffRequest, HospitalStaff>();
            CreateMap<UpdateHospitalStaffRequest, HospitalStaff>();

            // User mappings
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.HospitalStaff.FullName));
            CreateMap<User, UserDetailResponse>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();

            // MedicalRecord mappings
            CreateMap<MedicalRecord, MedicalRecordResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName));
            CreateMap<MedicalRecord, MedicalRecordDetailResponse>();
            CreateMap<CreateMedicalRecordRequest, MedicalRecord>();
            CreateMap<UpdateMedicalRecordRequest, MedicalRecord>();
            CreateMap<MedicalRecordCreateDto, MedicalRecord>();
            CreateMap<MedicalRecordUpdateDto, MedicalRecord>();

            // Service mappings
            CreateMap<Service, ServiceResponse>();
            CreateMap<CreateServiceRequest, Service>();
            CreateMap<UpdateServiceRequest, Service>();

            // ImagingRequest mappings
            CreateMap<ImagingRequest, ImagingRequestResponse>();
            CreateMap<ImagingRequest, ImagingRequestDetailResponse>();
            CreateMap<CreateImagingRequestRequest, ImagingRequest>();
            CreateMap<UpdateImagingRequestRequest, ImagingRequest>();

            // DiagnosisResult mappings
            CreateMap<DiagnosisResult, DiagnosisResultResponse>();
            CreateMap<DiagnosisResult, DiagnosisResultDetailResponse>();
            CreateMap<CreateDiagnosisResultRequest, DiagnosisResult>();
            CreateMap<UpdateDiagnosisResultRequest, DiagnosisResult>();

            // DicomImage mappings
            CreateMap<DicomImage, DicomImageResponse>();
            CreateMap<DicomImage, DicomImageDetailResponse>();
            CreateMap<CreateDicomImageRequest, DicomImage>();
            CreateMap<UpdateDicomImageRequest, DicomImage>();

            // Appointment mappings
            CreateMap<Appointment, AppointmentResponse>();
            CreateMap<CreateAppointmentRequest, Appointment>();
            CreateMap<UpdateAppointmentRequest, Appointment>();

            // Department mappings
            CreateMap<Department, DepartmentResponse>();
            CreateMap<CreateDepartmentRequest, Department>();
            CreateMap<UpdateDepartmentRequest, Department>();

            // Doctor mappings
            CreateMap<HospitalStaff, DoctorResponse>()
                .ForMember(dest => dest.DoctorID, opt => opt.MapFrom(src => src.StaffID))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Status == null || src.Status.ToLower() == "đang làm việc"))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Position));

            // PatientAssignment mappings
            CreateMap<PatientAssignment, PatientAssignmentResponse>();
            CreateMap<CreatePatientAssignmentRequest, PatientAssignment>();
        }
    }
}