using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Configurations;

public class RoomModelConfiguration : IEntityTypeConfiguration<RoomModel>
{
    public void Configure(EntityTypeBuilder<RoomModel> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Number)
            .IsRequired();

        builder.Property(_ => _.PricePerDay)
            .IsRequired();

        builder.HasOne(_ => _.Hotel)
            .WithMany(_ => _.Rooms);

        builder.ToTable("Rooms");
    }
}