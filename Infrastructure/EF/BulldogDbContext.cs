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
        public DbSet<Break> Breaks { get; set; }

        public BulldogDbContext() { }

        public BulldogDbContext(DbContextOptions<BulldogDbContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailableDate>()
                .HasKey(ad => ad.Id);

            modelBuilder.Entity<AvailableDate>()
                .OwnsOne(ad => ad.WorkingHours);

            modelBuilder.Entity<AvailableDate>()
                .HasMany(ad => ad.Breaks)
                .WithOne()
                .HasForeignKey(b => b.AvailableDateId);

            // Dodaj inne konfiguracje dla pozostałych klas

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MHJGK2O;Database=BulldogBarbers;Trusted_Connection=True;TrustServerCertificate=True;");
        }


    }
}
