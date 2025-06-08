using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class DiagnosisResult
    {
        [Key]
        public int DiagnosisResultID { get; set; }

        [Required]
        public int RecordID { get; set; }

        [StringLength(500)]
        public string DoctorConclusion { get; set; }

        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000)]
        public string ResultDescription { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        // Foreign keys
        [ForeignKey("RecordID")]
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}