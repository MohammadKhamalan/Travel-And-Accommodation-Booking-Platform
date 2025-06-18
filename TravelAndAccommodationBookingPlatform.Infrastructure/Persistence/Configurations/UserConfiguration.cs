using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(25);

            builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(25);

            builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(250);
          builder
          .HasIndex(user => user.Email)
          .IsUnique();
           
            builder
           .Property(user => user.PhoneNumber)
           .IsRequired()
           .HasMaxLength(20);

            builder.Property(u => u.Password).IsRequired();

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<UserRole>());
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
        }
    }
}
