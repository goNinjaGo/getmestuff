using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace getmestuff.Models
{
    /// <summary>
    /// Data transfer object for <see cref="CustomerProfile"/>
    /// </summary>
    public class CustomerProfileDto
    {
        public CustomerProfileDto() { }

        public CustomerProfileDto(CustomerProfile profile)
        {
            CustomerProfileId = profile.CustomerProfileId;
            UserId = profile.UserId;
            foreach (Order order in profile.Orders)
            {
                Orders.Add(new OrderDto(order));
            }
        }
        
        [Key]
        public int CustomerProfileId { get; set; }
        
        [Required]
        public string UserId { get; set; }

        public virtual List<OrderDto> Orders { get; set; }

        public CustomerProfile ToEntity()
        {
            CustomerProfile profile = new CustomerProfile
            {
                CustomerProfileId = CustomerProfileId,
                UserId = UserId
            };
            foreach (OrderDto order in Orders)
            {
                profile.Orders.Add(order.ToEntity());
            }
            return profile;
        }
    }
}