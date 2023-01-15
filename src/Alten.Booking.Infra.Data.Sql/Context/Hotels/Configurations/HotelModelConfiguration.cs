using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Configurations;

public class HotelModelConfiguration : IEntityTypeConfiguration<HotelModel>
{
    public void Configure(EntityTypeBuilder<HotelModel> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.Description)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(_ => _.CreateDate)
            .IsRequired();

        builder.Property(_ => _.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.City)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(_ => _.State)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(_ => _.Address)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.Active)
            .IsRequired();

        builder.HasMany(_ => _.Rooms)
            .WithOne(_ => _.Hotel)
            .HasForeignKey(_ => _.HotelId);

        builder.ToTable("Hotels");
    }
}