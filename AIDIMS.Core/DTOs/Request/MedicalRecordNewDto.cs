using System.ComponentModel.DataAnnotations;
using AIDIMS.Core.Models.Enums;

namespace AIDIMS.Core.DTOs.Request
{
    public class MedicalRecordCreateDto
    {
        [Required]
        public int PatientID { get; set; }
        [Required]
        public int AppointmentID { get; set; }
        public string Symptoms { get; set; }
    }

    public class MedicalRecordUpdateDto
    {
        public string InitialDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public MedicalRecordStatus? Status { get; set; }
    }
}