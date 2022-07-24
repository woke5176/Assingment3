using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace assignment3New.Models
{
    public partial class AirlineBookingContext : DbContext
    {
        public AirlineBookingContext()
        {
        }

        public AirlineBookingContext(DbContextOptions<AirlineBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; } = null!;
        public virtual DbSet<Airplane> Airplanes { get; set; } = null!;
        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<ContactDetail> ContactDetails { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<FlightInstance> FlightInstances { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<RoutePlane> RoutePlanes { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=AirlineBooking;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True ;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasKey(e => e.AirlineCode);

                entity.ToTable("Airline");

                entity.Property(e => e.AirlineCode)
                    .ValueGeneratedNever()
                    .HasColumnName("Airline_code");

                entity.Property(e => e.AirlineName)
                    .HasMaxLength(50)
                    .HasColumnName("Airline_name");
            });

            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.ToTable("Airplane");

                entity.Property(e => e.AirplaneId)
                    .ValueGeneratedNever()
                    .HasColumnName("Airplane_ID");

                entity.Property(e => e.AirplaneName)
                    .HasMaxLength(50)
                    .HasColumnName("Airplane_name");

                entity.Property(e => e.BSeats).HasColumnName("B_seats");

                entity.Property(e => e.ESeats).HasColumnName("E_seats");

                entity.Property(e => e.FSeats).HasColumnName("F_seats");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.AirportCode);

                entity.ToTable("Airport");

                entity.Property(e => e.AirportCode)
                    .ValueGeneratedNever()
                    .HasColumnName("Airport_code");

                entity.Property(e => e.AirportName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Airport_name");

                entity.Property(e => e.CityCode).HasColumnName("City_code");

                entity.Property(e => e.CountryCode).HasColumnName("Country_code");

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_City");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_Country");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.ToTable("City");

                entity.Property(e => e.CityCode)
                    .ValueGeneratedNever()
                    .HasColumnName("City_code");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("City_name");

                entity.Property(e => e.CountryCode).HasColumnName("Country_code");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<ContactDetail>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("Contact_Details");

                entity.Property(e => e.ContactId)
                    .ValueGeneratedNever()
                    .HasColumnName("Contact_ID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Line1).HasMaxLength(50);

                entity.Property(e => e.Line2).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode);

                entity.ToTable("Country");

                entity.Property(e => e.CountryCode)
                    .ValueGeneratedNever()
                    .HasColumnName("Country_code");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Country_name");
            });

            modelBuilder.Entity<FlightInstance>(entity =>
            {
                entity.HasKey(e => e.InstanceId);

                entity.ToTable("Flight_Instances");

                entity.Property(e => e.InstanceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Instance_ID");

                entity.Property(e => e.Arrival).HasColumnType("datetime");

                entity.Property(e => e.BCost).HasColumnName("B_cost");

                entity.Property(e => e.BSeat).HasColumnName("B_seat");

                entity.Property(e => e.Departure).HasColumnType("datetime");

                entity.Property(e => e.ECost).HasColumnName("E_cost");

                entity.Property(e => e.ESeat).HasColumnName("E_seat");

                entity.Property(e => e.FCost).HasColumnName("F_cost");

                entity.Property(e => e.FSeat).HasColumnName("F_seat");

                entity.Property(e => e.PlaneId).HasColumnName("Plane_ID");

                entity.Property(e => e.RouteId).HasColumnName("Route_ID");

                entity.HasOne(d => d.Plane)
                    .WithMany(p => p.FlightInstances)
                    .HasForeignKey(d => d.PlaneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Instances_Airplane");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.FlightInstances)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Instances_Routes");

                entity.HasOne(d => d.RouteNavigation)
                    .WithMany(p => p.FlightInstances)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Instances_Route_Plane");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.PassengerId)
                    .ValueGeneratedNever()
                    .HasColumnName("Passenger_ID");

                entity.Property(e => e.Cancelled)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Confirmed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_ID");

                entity.Property(e => e.FlightInstId).HasColumnName("Flight_inst_ID");

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Passenger_name");

                entity.Property(e => e.SeatNo).HasColumnName("Seat_no");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.FlightInst)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.FlightInstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passengers_Flight_Instances");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passengers_Users");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.RouteId)
                    .ValueGeneratedNever()
                    .HasColumnName("Route_ID");

                entity.Property(e => e.AirlineCode).HasColumnName("Airline_code");

                entity.Property(e => e.ArrivalAirportCode).HasColumnName("Arrival_airport_code");

                entity.Property(e => e.DepartureAirportCode).HasColumnName("Departure_airport_code");

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Airline");

                entity.HasOne(d => d.ArrivalAirportCodeNavigation)
                    .WithMany(p => p.RouteArrivalAirportCodeNavigations)
                    .HasForeignKey(d => d.ArrivalAirportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Airport1");

                entity.HasOne(d => d.DepartureAirportCodeNavigation)
                    .WithMany(p => p.RouteDepartureAirportCodeNavigations)
                    .HasForeignKey(d => d.DepartureAirportCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Airport");

                entity.HasOne(d => d.RouteNavigation)
                    .WithOne(p => p.Route)
                    .HasForeignKey<Route>(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Route_Plane");
            });

            modelBuilder.Entity<RoutePlane>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.ToTable("Route_Plane");

                entity.Property(e => e.RouteId)
                    .ValueGeneratedNever()
                    .HasColumnName("Route_ID");

                entity.Property(e => e.PlaneId).HasColumnName("Plane_ID");

                entity.HasOne(d => d.Plane)
                    .WithMany(p => p.RoutePlanes)
                    .HasForeignKey(d => d.PlaneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_Plane_Airplane");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Order_ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Gateway)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Payment_mode");

                entity.Property(e => e.Respmsg)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("User_ID");

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("User_name");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Contact_Details");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
