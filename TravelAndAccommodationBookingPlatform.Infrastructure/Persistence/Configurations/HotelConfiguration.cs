using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Name).IsRequired()
            .HasMaxLength(20);
        builder.Property(h => h.StreetAddress).IsRequired()
           .HasMaxLength(20);
        builder.Property(h => h.Description).IsRequired()
           .HasMaxLength(250);

        builder.Property(h => h.NumberOfRooms).IsRequired();

        builder.Property(h => h.PhoneNumber).IsRequired()
           .HasMaxLength(20);

        builder.Property(h => h.Rating).IsRequired();

        builder.HasOne(c => c.City)
            .WithMany(h => h.Hotels)
            .HasForeignKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Owner)
           .WithMany(o => o.Hotels)
           .HasForeignKey(h => h.OwnerId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(i => i.Images).WithOne(h => h.Hotel)
            .HasForeignKey(i => i.HotelId).OnDelete(DeleteBehavior.Cascade);
       
    }
    }
