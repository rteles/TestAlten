using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Models.Configurations;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.LastName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(o => o.BirthDate)
            .IsRequired();

        builder.Property(o => o.Cpf)
            .IsRequired()
            .HasMaxLength(14);
        
        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.Active)
            .IsRequired();

        builder.ToTable("Users");
    }
}