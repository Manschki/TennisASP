using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TennisDB
{
    public class TennisContext : DbContext
    {
        public TennisContext() { }
        public TennisContext(DbContextOptions<TennisContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "data source=(LocalDB)\\mssqllocaldb;attachdbfilename=C:\\Temp\\NorthwindWebAPIDb\\Tennis.mdf;integrated security=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
