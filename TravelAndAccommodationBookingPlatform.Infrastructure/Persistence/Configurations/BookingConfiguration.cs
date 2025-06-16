using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CheckInDate).IsRequired().HasColumnType("datetime");
        builder.Property(b => b.CheckOutDate).IsRequired().HasColumnType("datetime");
        builder.HasOne(b => b.User)
           .WithMany(u => u.Bookings)
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Room)
 .WithMany(r => r.Bookings)
 .HasForeignKey(b => b.RoomId)
 .OnDelete(DeleteBehavior.Cascade);

        builder
      .Property(booking => booking.Price)
      .IsRequired();

        builder.HasOne(b => b.Review)
              .WithOne(r => r.Booking)
              .HasForeignKey<Review>(r => r.BookingId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Payment)
              .WithOne(r => r.Booking)
              .HasForeignKey<Review>(r => r.BookingId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => b.UserId);
        builder.HasIndex(b => b.CheckInDate);
        builder.HasIndex(b => b.CheckOutDate);


        builder
               .HasCheckConstraint(
                   "CK_Booking_CheckInDate",
                   "CheckInDate >= GETDATE()");

       
        builder
            .HasCheckConstraint(
                "CK_Booking_CheckOutDate",
                "CheckOutDate > CheckInDate");

    }
    }
