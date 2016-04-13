using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace getmestuff.Models
{
    public class GetMeStuffContext : DbContext
    {
        public GetMeStuffContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<OrderNote> OrderNotes { get; set; }
    }
}