using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIDIMS.Core.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        // Foreign keys
        [ForeignKey("StaffID")]
        public virtual HospitalStaff HospitalStaff { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}