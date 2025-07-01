using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới Patient
    /// </summary>
    public class CreatePatientRequest
    {
        [Required(ErrorMessage = "Tên bệnh nhân là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên bệnh nhân không được vượt quá 100 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime BirthDate { get; set; }

        [StringLength(20, ErrorMessage = "Giới tính không được vượt quá 20 ký tự")]
        public string Gender { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật Patient
    /// </summary>
    public class UpdatePatientRequest
    {
        [Required(ErrorMessage = "Tên bệnh nhân là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên bệnh nhân không được vượt quá 100 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime BirthDate { get; set; }

        [StringLength(20, ErrorMessage = "Giới tính không được vượt quá 20 ký tự")]
        public string Gender { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}