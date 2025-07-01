using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một hình ảnh DICOM
    /// </summary>
    public class DicomImageResponse
    {
        public int ImageID { get; set; }
        public int RecordID { get; set; }
        public int? TechnicianID { get; set; }
        public string DoctorNotes { get; set; }
        public string AIFeedback { get; set; }
        public bool IsApproved { get; set; }
        public string FilePath { get; set; }
        public string ImageType { get; set; }
        public long FileSize { get; set; }
        public string Resolution { get; set; }
        public DateTime CreatedDate { get; set; }

        // Thông tin liên quan
        public string PatientName { get; set; }
        public string TechnicianName { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về thông tin chi tiết của một hình ảnh DICOM
    /// </summary>
    public class DicomImageDetailResponse : DicomImageResponse
    {
        public MedicalRecordResponse MedicalRecord { get; set; }
        public HospitalStaffResponse Technician { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các hình ảnh DICOM
    /// </summary>
    public class DicomImageListResponse
    {
        public List<DicomImageResponse> DicomImages { get; set; } = new List<DicomImageResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với DicomImage
    /// </summary>
    public class DicomImageActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DicomImageResponse DicomImage { get; set; }
    }
}