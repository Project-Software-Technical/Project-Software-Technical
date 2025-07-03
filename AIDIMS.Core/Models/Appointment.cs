using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

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

        // Navigation
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        public virtual PatientAssignment PatientAssignment { get; set; }
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}