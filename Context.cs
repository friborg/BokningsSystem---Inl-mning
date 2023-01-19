using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BokningsSystem___Inlämning.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BokningsSystem___Inlämning
{
    public class Context : DbContext
    {
        public Context() { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<BookedRoom> Bookedrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = tcp:hanna.database.windows.net, 1433; Initial Catalog = bookingappfriborg; Persist Security Info = False; User ID = hannaadmin; Password = Lösenord1; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }
    }
}
