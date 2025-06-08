using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [StringLength(500)]
        public string Symptoms { get; set; }

        public DateTime ExaminationDate { get; set; }

        [StringLength(500)]
        public string Diagnosis { get; set; }

        [StringLength(500)]
        public string Result { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        // Foreign keys
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("StaffID")]
        public virtual HospitalStaff HospitalStaff { get; set; }

        // Navigation properties
        public virtual ICollection<ImagingRequest> ImagingRequests { get; set; }
        public virtual ICollection<DiagnosisResult> DiagnosisResults { get; set; }
        public virtual ICollection<DicomImage> DicomImages { get; set; }
    }
}