using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BokningsSystem___Inlämning.Models;
using Microsoft.EntityFrameworkCore;

namespace BokningsSystem___Inlämning
{
    public class Context : DbContext
    {
        public Context() { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelRoom> Hotelrooms { get; set; }
        public DbSet<BookedRoom> Bookedrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=BookingApplication;
                Trusted_Connection=True;");
            }
        }
    }
}
