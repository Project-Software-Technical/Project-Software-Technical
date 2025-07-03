using System;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    public class CreateAppointmentRequest
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(100)]
        public string Department { get; set; }
    }

    public class UpdateAppointmentRequest : CreateAppointmentRequest { }

    public class UpdateAppointmentStatusRequest
    {
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}