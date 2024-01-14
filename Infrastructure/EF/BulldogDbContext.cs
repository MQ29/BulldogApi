using Bulldog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.EF
{
    public class BulldogDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AvailableDate> AvailableDates { get; set; }

        public BulldogDbContext(DbContextOptions<BulldogDbContext> options) :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MHJGK2O;Database=BulldogBarbers;Trusted_Connection=True;TrustServerCertificate=True;");
        }


    }
}
