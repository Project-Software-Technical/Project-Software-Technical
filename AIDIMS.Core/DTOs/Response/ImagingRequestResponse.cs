using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một yêu cầu chụp
    /// </summary>
    public class ImagingRequestResponse
    {
        public int RequestID { get; set; }
        public int RecordID { get; set; }
        public int ServiceID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public int? TechnicianID { get; set; }
        public string TechnicianName { get; set; }

        // Thông tin liên quan
        public string ServiceName { get; set; }
        public string PatientName { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về thông tin chi tiết của một yêu cầu chụp
    /// </summary>
    public class ImagingRequestDetailResponse : ImagingRequestResponse
    {
        public MedicalRecordResponse MedicalRecord { get; set; }
        public ServiceResponse Service { get; set; }
        public List<DicomImageResponse> DicomImages { get; set; } = new List<DicomImageResponse>();
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các yêu cầu chụp
    /// </summary>
    public class ImagingRequestListResponse
    {
        public List<ImagingRequestResponse> ImagingRequests { get; set; } = new List<ImagingRequestResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với ImagingRequest
    /// </summary>
    public class ImagingRequestActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ImagingRequestResponse ImagingRequest { get; set; }
    }
}