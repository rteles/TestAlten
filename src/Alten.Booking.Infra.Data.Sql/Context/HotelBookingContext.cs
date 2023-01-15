using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;
using Alten.Booking.Infra.Data.Sql.Context.Users.Seed;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context;

public class HotelBookingContext : DbContext
{
    public HotelBookingContext(DbContextOptions<HotelBookingContext> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<HotelModel> Hotels { get; set; }
    public DbSet<RoomModel> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelBookingContext).Assembly);
        modelBuilder.Entity<UserModel>()
            .HasData(UsersDataSeed.Seed);

        base.OnModelCreating(modelBuilder);
    }
}