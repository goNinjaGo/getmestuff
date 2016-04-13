using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace getmestuff.Models
{
    /// <summary>
    /// Data transfer object for <see cref="LineItem"/>
    /// </summary>
    public class LineItemDto
    {
        public LineItemDto() { }

        public LineItemDto(LineItem item)
        {
            LineItemId = item.LineItemId;
            OrderId = item.OrderId;
            ImageUrl = item.ImageUrl;
            AmazonUrl = item.AmazonUrl;
            Description = item.Description;
            Quantity = item.Quantity;
            EstimatedCost = item.EstimatedCost;
            ActualCost = item.ActualCost;
            Notes = item.Notes;
            Removed = item.Removed;
        }

        [Key]
        public int LineItemId { get; set; }

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

        public LineItem ToEntity()
        {
            return new LineItem
            {
                LineItemId = LineItemId,
                OrderId = OrderId,
                ImageUrl = ImageUrl,
                AmazonUrl = AmazonUrl,
                Description = Description,
                Quantity = Quantity,
                EstimatedCost = EstimatedCost,
                ActualCost = ActualCost,
                Notes = Notes,
                Removed = Removed
            };
        }
    }
}