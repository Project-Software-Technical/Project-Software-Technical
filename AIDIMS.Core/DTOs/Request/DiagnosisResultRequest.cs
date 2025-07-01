using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới DiagnosisResult
    /// </summary>
    public class CreateDiagnosisResultRequest
    {
        [Required(ErrorMessage = "Mã hồ sơ bệnh án là bắt buộc")]
        public int RecordID { get; set; }

        [StringLength(500, ErrorMessage = "Kết luận bác sĩ không được vượt quá 500 ký tự")]
        public string DoctorConclusion { get; set; }

        [Required(ErrorMessage = "Ngày chẩn đoán là bắt buộc")]
        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả kết quả không được vượt quá 1000 ký tự")]
        public string ResultDescription { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string Notes { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật DiagnosisResult
    /// </summary>
    public class UpdateDiagnosisResultRequest
    {
        [StringLength(500, ErrorMessage = "Kết luận bác sĩ không được vượt quá 500 ký tự")]
        public string DoctorConclusion { get; set; }

        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả kết quả không được vượt quá 1000 ký tự")]
        public string ResultDescription { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string Notes { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }
    }
}