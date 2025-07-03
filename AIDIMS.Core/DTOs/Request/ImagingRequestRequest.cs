using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới ImagingRequest
    /// </summary>
    public class CreateImagingRequestRequest
    {
        [Required(ErrorMessage = "Mã hồ sơ bệnh án là bắt buộc")]
        public int RecordID { get; set; }

        [Required(ErrorMessage = "Mã dịch vụ là bắt buộc")]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Mã kỹ thuật viên là bắt buộc")]
        public int TechnicianID { get; set; }

        [Required(ErrorMessage = "Ngày yêu cầu là bắt buộc")]
        public DateTime RequestDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string Notes { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật ImagingRequest
    /// </summary>
    public class UpdateImagingRequestRequest
    {
        public int ServiceID { get; set; }

        public DateTime? ExecutionDate { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string Notes { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }

        public int? TechnicianID { get; set; }
    }
}