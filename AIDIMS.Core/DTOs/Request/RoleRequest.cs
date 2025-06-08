using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới Role
    /// </summary>
    public class CreateRoleRequest
    {
        [Required(ErrorMessage = "Tên vai trò là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên vai trò không được vượt quá 50 ký tự")]
        public string RoleName { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string Description { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật Role
    /// </summary>
    public class UpdateRoleRequest
    {

        [Required(ErrorMessage = "Tên vai trò là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên vai trò không được vượt quá 50 ký tự")]
        public string RoleName { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá 255 ký tự")]
        public string Description { get; set; }
    }
}