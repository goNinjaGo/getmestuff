using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace getmestuff.Models
{
    /// <summary>
    /// Data transfer object for <see cref="Order"/>
    /// </summary>
    public class OrderDto
    {
        public OrderDto() { }

        public OrderDto(Order order)
        {
            OrderId = order.OrderId;
            UserId = order.UserId;
            StreetAddress = order.StreetAddress;
            UnitDesignation = order.UnitDesignation;
            City = order.City;
            Province = order.Province;
            PostalCode = order.PostalCode;
            Country = order.Country;
            TaxEstimate = order.TaxEstimate;
            ServiceFee = order.ServiceFee;
            DriverTip = order.DriverTip;
            TotalEstimate = order.TotalEstimate;
            TotalExact = order.TotalExact;
            CreatedDate = order.CreatedDate;
            AcceptedById = order.AcceptedById;
            AcceptedDate = order.AcceptedDate;
            ApprovedDate = order.ApprovedDate;
            CompletedDate = order.CompletedDate;
            foreach (LineItem item in order.LineItems)
            {
                LineItems.Add(new LineItemDto(item));
            }
            foreach (OrderNote note in order.OrderNotes)
            {
                OrderNotes.Add(new OrderNoteDto(note));
            }
        }

        [Key]
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

        public virtual List<LineItemDto> LineItems { get; set; }

        public virtual List<OrderNoteDto> OrderNotes { get; set; }

        public Order ToEntity()
        {
            Order order = new Order
            {
                OrderId = OrderId,
                UserId = UserId,
                StreetAddress = StreetAddress,
                UnitDesignation = UnitDesignation,
                City = City,
                Province = Province,
                PostalCode = PostalCode,
                Country = Country,
                TaxEstimate = TaxEstimate,
                ServiceFee = ServiceFee,
                DriverTip = DriverTip,
                TotalEstimate = TotalEstimate,
                TotalExact = TotalExact,
                CreatedDate = CreatedDate,
                AcceptedById = AcceptedById,
                AcceptedDate = AcceptedDate,
                ApprovedDate = ApprovedDate,
                CompletedDate = CompletedDate
            };
            foreach (LineItemDto item in LineItems)
            {
                order.LineItems.Add(item.ToEntity());
            }
            foreach (OrderNoteDto note in OrderNotes)
            {
                order.OrderNotes.Add(note.ToEntity());
            }

            return order;
        }
    }
}