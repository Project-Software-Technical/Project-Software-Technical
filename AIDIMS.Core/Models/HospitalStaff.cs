using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class HospitalStaff
    {
        [Key]
        public int StaffID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        // New fields
        public int? DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<DicomImage> DicomImages { get; set; }
        public virtual ICollection<PatientAssignment> PatientAssignments { get; set; }
    }
}