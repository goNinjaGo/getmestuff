using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace getmestuff.Models
{
    /// <summary>
    /// Order Entity
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        public string UnitDesignation { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        public decimal TaxEstimate { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal DriverTip { get; set; }

        public decimal TotalEstimate { get; set; }        

        public decimal TotalExact { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }

        public int AcceptedById { get; set; }

        public DateTime AcceptedDate { get; set; }

        public DateTime ApprovedDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public virtual List<LineItem> LineItems { get; set; }

        public virtual List<OrderNote> OrderNotes { get; set; }
    }
}