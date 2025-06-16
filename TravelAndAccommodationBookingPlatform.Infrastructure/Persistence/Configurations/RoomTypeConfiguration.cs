using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasIndex(roomType => roomType.RoomCategory);

       
        builder.HasKey(rt => rt.RoomTypeId);

        builder.Property(rt => rt.RoomCategory)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<RoomCategory>());


        builder.Property(rt => rt.PricePerNight)
            .IsRequired();
           

       
        builder.HasMany(rt => rt.Amenities)
            .WithMany(a => a.RoomTypes);

      
        builder.HasMany(rt => rt.Discounts)
            .WithOne(d => d.RoomType)
            .HasForeignKey(d => d.RoomTypeId)
            .OnDelete(DeleteBehavior.Cascade);

       
        builder.HasMany(rt => rt.Rooms)
            .WithOne(r => r.RoomType)
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(roomType =>
            roomType.HasCheckConstraint("CK_RoomType_PriceRange", "[PricePerNight] >= 0"));
    }
    }
