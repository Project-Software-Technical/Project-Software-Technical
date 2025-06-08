using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class ImagingRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public int RecordID { get; set; }

        [Required]
        public int ServiceID { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        // Foreign keys
        [ForeignKey("RecordID")]
        public virtual MedicalRecord MedicalRecord { get; set; }

        [ForeignKey("ServiceID")]
        public virtual Service Service { get; set; }
    }
}