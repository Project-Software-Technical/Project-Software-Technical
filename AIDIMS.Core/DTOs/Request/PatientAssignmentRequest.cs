using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.DTOs.Request
{
    public class CreatePatientAssignmentRequest
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        public int? AppointmentID { get; set; }
        public int? MedicalRecordID { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}