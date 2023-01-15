using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Models.Configurations;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(_ => _.BirthDate)
            .IsRequired();

        builder.Property(_ => _.Cpf)
            .IsRequired()
            .HasMaxLength(14);
        
        builder.Property(_ => _.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(_ => _.Active)
            .IsRequired();

        builder.ToTable("Users");
    }
}