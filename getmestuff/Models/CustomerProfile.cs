using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace getmestuff.Models
{
    /// <summary>
    /// CustomerProfile Entity
    /// </summary>
    public class CustomerProfile
    {
        public int CustomerProfileId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}