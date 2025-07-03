using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO tạo mới Department
    /// </summary>
    public class CreateDepartmentRequest
    {
        [Required(ErrorMessage = "Mã khoa là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã khoa không được vượt quá 20 ký tự")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Tên khoa là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// DTO cập nhật Department
    /// </summary>
    public class UpdateDepartmentRequest
    {
        [Required(ErrorMessage = "Mã khoa là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã khoa không được vượt quá 20 ký tự")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Tên khoa là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}