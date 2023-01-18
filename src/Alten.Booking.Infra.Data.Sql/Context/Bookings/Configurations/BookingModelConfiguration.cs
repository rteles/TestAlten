using Alten.Booking.Infra.Data.Sql.Context.Bookings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.Booking.Infra.Data.Sql.Context.Bookings.Configurations;

public class BookingModelConfiguration: IEntityTypeConfiguration<BookingModel>
{
    public void Configure(EntityTypeBuilder<BookingModel> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.HasOne(_ => _.User)
            .WithMany(_ => _.Bookings)
            .HasForeignKey(_=>_.UserId);

        builder.HasOne(_ => _.Room)
            .WithMany(_ => _.Bookings)
            .HasForeignKey(_=>_.RoomId);
        
        builder.Property(_ => _.CheckinDate)
            .IsRequired();
        
        builder.Property(_ => _.CheckoutDate)
            .IsRequired();
        
        builder.Property(_ => _.PricePerDay)
            .IsRequired();
        
        builder.Property(_ => _.TotalPrice)
            .IsRequired();
        
        builder.Property(_ => _.Active)
            .IsRequired();

        builder.ToTable("Bookings");
    }
}