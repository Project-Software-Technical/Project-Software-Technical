using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<User> Users { get; set; }
    }
}