using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class PatientAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        public int? AppointmentID { get; set; }

        public int? MedicalRecordID { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? Note { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Assigned";

        // Navigation properties
        [ForeignKey("PatientID")] public virtual Patient Patient { get; set; }
        [ForeignKey("DoctorID")] public virtual HospitalStaff Doctor { get; set; }
        [ForeignKey("AppointmentID")] public virtual Appointment Appointment { get; set; }
        [ForeignKey("MedicalRecordID")] public virtual MedicalRecord MedicalRecord { get; set; }
    }
}