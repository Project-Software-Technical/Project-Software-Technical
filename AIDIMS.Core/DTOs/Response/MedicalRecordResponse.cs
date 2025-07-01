using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một hồ sơ bệnh án
    /// </summary>
    public class MedicalRecordResponse
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int StaffID { get; set; }
        public string Symptoms { get; set; }
        public DateTime ExaminationDate { get; set; }
        public string Diagnosis { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        // Thông tin liên quan
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về thông tin chi tiết của một hồ sơ bệnh án
    /// </summary>
    public class MedicalRecordDetailResponse : MedicalRecordResponse
    {
        public PatientResponse Patient { get; set; }
        public HospitalStaffResponse Doctor { get; set; }
        public List<ImagingRequestResponse> ImagingRequests { get; set; } = new List<ImagingRequestResponse>();
        public List<DiagnosisResultResponse> DiagnosisResults { get; set; } = new List<DiagnosisResultResponse>();
        public List<DicomImageResponse> DicomImages { get; set; } = new List<DicomImageResponse>();
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các hồ sơ bệnh án
    /// </summary>
    public class MedicalRecordListResponse
    {
        public List<MedicalRecordResponse> MedicalRecords { get; set; } = new List<MedicalRecordResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với MedicalRecord
    /// </summary>
    public class MedicalRecordActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public MedicalRecordResponse MedicalRecord { get; set; }
    }
}