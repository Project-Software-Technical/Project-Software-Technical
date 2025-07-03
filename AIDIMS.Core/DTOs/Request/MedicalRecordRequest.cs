using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    /// <summary>
    /// DTO để tạo mới MedicalRecord
    /// </summary>
    public class CreateMedicalRecordRequest
    {
        [Required(ErrorMessage = "Mã bệnh nhân là bắt buộc")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Mã bác sĩ là bắt buộc")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Mã lịch hẹn là bắt buộc")]
        public int AppointmentID { get; set; }

        [StringLength(500, ErrorMessage = "Triệu chứng không được vượt quá 500 ký tự")]
        public string Symptoms { get; set; }

        [Required(ErrorMessage = "Ngày khám là bắt buộc")]
        public DateTime ExaminationDate { get; set; }

        [StringLength(500, ErrorMessage = "Chẩn đoán không được vượt quá 500 ký tự")]
        public string Diagnosis { get; set; }

        [StringLength(500, ErrorMessage = "Kết quả không được vượt quá 500 ký tự")]
        public string Result { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }
    }

    /// <summary>
    /// DTO để cập nhật MedicalRecord
    /// </summary>
    public class UpdateMedicalRecordRequest
    {
        [Required(ErrorMessage = "Mã bác sĩ là bắt buộc")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Mã lịch hẹn là bắt buộc")]
        public int AppointmentID { get; set; }

        [StringLength(500, ErrorMessage = "Triệu chứng không được vượt quá 500 ký tự")]
        public string Symptoms { get; set; }

        [Required(ErrorMessage = "Ngày khám là bắt buộc")]
        public DateTime ExaminationDate { get; set; }

        [StringLength(500, ErrorMessage = "Chẩn đoán không được vượt quá 500 ký tự")]
        public string Diagnosis { get; set; }

        [StringLength(500, ErrorMessage = "Kết quả không được vượt quá 500 ký tự")]
        public string Result { get; set; }

        [StringLength(20, ErrorMessage = "Trạng thái không được vượt quá 20 ký tự")]
        public string Status { get; set; }
    }
}