using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.LastName)
            .HasMaxLength(50);

        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.PhoneNumber)
            .HasMaxLength(20);

        builder.HasMany(o => o.Hotels)
            .WithOne(h => h.Owner)
            .HasForeignKey(h => h.OwnerId);
    }
}
