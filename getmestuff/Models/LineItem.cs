using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace getmestuff.Models
{
    /// <summary>
    /// LineItem Entity
    /// </summary>
    public class LineItem
    {
        public int LineItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public string ImageUrl { get; set; }

        public string AmazonUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal EstimatedCost { get; set; }

        public decimal ActualCost { get; set; }

        public string Notes { get; set; }

        public bool Removed { get; set; }

        public virtual Order Order { get; set; }
    }
}