using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới DicomImage
    /// </summary>
    public class CreateDicomImageRequest
    {
        [Required(ErrorMessage = "Mã hồ sơ bệnh án là bắt buộc")]
        public int RecordID { get; set; }

        public int? TechnicianID { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú bác sĩ không được vượt quá 500 ký tự")]
        public string DoctorNotes { get; set; }

        [StringLength(500, ErrorMessage = "Phản hồi AI không được vượt quá 500 ký tự")]
        public string AIFeedback { get; set; }

        public bool IsApproved { get; set; }

        [Required(ErrorMessage = "Đường dẫn file là bắt buộc")]
        [StringLength(255, ErrorMessage = "Đường dẫn file không được vượt quá 255 ký tự")]
        public string FilePath { get; set; }

        [StringLength(50, ErrorMessage = "Loại hình ảnh không được vượt quá 50 ký tự")]
        public string ImageType { get; set; }

        public long FileSize { get; set; }

        [StringLength(50, ErrorMessage = "Độ phân giải không được vượt quá 50 ký tự")]
        public string Resolution { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật DicomImage
    /// </summary>
    public class UpdateDicomImageRequest
    {
        public int? TechnicianID { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú bác sĩ không được vượt quá 500 ký tự")]
        public string DoctorNotes { get; set; }

        [StringLength(500, ErrorMessage = "Phản hồi AI không được vượt quá 500 ký tự")]
        public string AIFeedback { get; set; }

        public bool IsApproved { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn file không được vượt quá 255 ký tự")]
        public string FilePath { get; set; }
    }
}