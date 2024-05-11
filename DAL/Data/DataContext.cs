using BookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .Property(a => a.PricePerNight)
                .HasColumnType("decimal(18, 2)");

            //modelBuilder.Entity<AccommodationReservation>()
            //     .HasKey(ar => new { ar.AccommodationId, ar.ReservationId });

            //modelBuilder.Entity<AccommodationReservation>()
            //    .HasOne(a => a.Accommodation)
            //    .WithMany(ar => ar.AccommodationReservations)
            //    .HasForeignKey(a => a.AccommodationId);

            //modelBuilder.Entity<AccommodationReservation>()
            //    .HasOne(r => r.Reservation)
            //    .WithMany(ar => ar.AccommodationReservations)
            //    .HasForeignKey(r => r.ReservationId);
        }
    }
}
