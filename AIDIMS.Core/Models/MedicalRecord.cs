using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AIDIMS.Core.Models.Enums;

namespace AIDIMS.Core.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? DoctorID { get; set; }

        public int AppointmentID { get; set; }

        public int? DepartmentID { get; set; }

        [StringLength(500)]
        public string Symptoms { get; set; }

        public DateTime ExaminationDate { get; set; }

        [StringLength(500)]
        public string InitialDiagnosis { get; set; }

        [StringLength(500)]
        public string FinalDiagnosis { get; set; }

        [StringLength(500)]
        public string Treatment { get; set; }

        [StringLength(500)]
        public string Prescription { get; set; }

        [StringLength(20)]
        public string Priority { get; set; } = "Normal";

        public MedicalRecordStatus Status { get; set; } = MedicalRecordStatus.New;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual HospitalStaff Receptionist { get; set; }

        [ForeignKey("DoctorID")]
        public virtual HospitalStaff Doctor { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        [ForeignKey("AppointmentID")]
        public virtual Appointment Appointment { get; set; }

        public virtual ICollection<ImagingRequest> ImagingRequests { get; set; }
        public virtual ICollection<DicomImage> DicomImages { get; set; }
        public virtual ICollection<DiagnosisResult> DiagnosisResults { get; set; }
        public virtual PatientAssignment PatientAssignment { get; set; }
    }
}