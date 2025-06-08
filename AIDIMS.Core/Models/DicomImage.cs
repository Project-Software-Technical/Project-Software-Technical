using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class DicomImage
    {
        [Key]
        public int ImageID { get; set; }

        [Required]
        public int RecordID { get; set; }

        public int? TechnicianID { get; set; }

        [StringLength(500)]
        public string DoctorNotes { get; set; }

        [StringLength(500)]
        public string AIFeedback { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }

        [StringLength(50)]
        public string ImageType { get; set; }

        public long FileSize { get; set; }

        [StringLength(50)]
        public string Resolution { get; set; }

        public DateTime CreatedDate { get; set; }

        // Foreign keys
        [ForeignKey("RecordID")]
        public virtual MedicalRecord MedicalRecord { get; set; }

        [ForeignKey("TechnicianID")]
        public virtual HospitalStaff Technician { get; set; }
    }
}