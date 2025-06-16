using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.AdultsCapacity).IsRequired();
        builder.Property(r => r.ChildrenCapacity).IsRequired();
        builder.Property(r => r.Rating).IsRequired();

        builder.HasMany(b => b.Bookings)
                 .WithOne(r => r.Room)
                 .HasForeignKey(r => r.RoomId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(r => r.Hotel)
           .WithMany(h => h.Room)
           .HasForeignKey(r => r.HotelId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.RoomType)
           .WithMany(rt => rt.Rooms)
           .HasForeignKey(r => r.RoomTypeId)
           .OnDelete(DeleteBehavior.SetNull);
        builder.HasIndex(r => r.HotelId);
        builder.HasIndex(r => r.RoomTypeId);

        builder.ToTable(room =>
            room
            .HasCheckConstraint
            ("CK_Review_RatingRange", "[Rating] >= 0 AND [Rating] <= 5"));
    }
}

