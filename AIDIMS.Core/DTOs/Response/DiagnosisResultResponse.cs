using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một kết quả chẩn đoán
    /// </summary>
    public class DiagnosisResultResponse
    {
        public int DiagnosisResultID { get; set; }
        public int RecordID { get; set; }
        public string DoctorConclusion { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ResultDescription { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        // Thông tin liên quan
        public string PatientName { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về thông tin chi tiết của một kết quả chẩn đoán
    /// </summary>
    public class DiagnosisResultDetailResponse : DiagnosisResultResponse
    {
        public MedicalRecordResponse MedicalRecord { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các kết quả chẩn đoán
    /// </summary>
    public class DiagnosisResultListResponse
    {
        public List<DiagnosisResultResponse> DiagnosisResults { get; set; } = new List<DiagnosisResultResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với DiagnosisResult
    /// </summary>
    public class DiagnosisResultActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DiagnosisResultResponse DiagnosisResult { get; set; }
    }
}