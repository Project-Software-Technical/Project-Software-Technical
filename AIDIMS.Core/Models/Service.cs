using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIDIMS.Core.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual ICollection<ImagingRequest> ImagingRequests { get; set; }
    }
}