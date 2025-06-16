using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).IsRequired();
            builder.HasOne(b => b.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Status).IsRequired().HasConversion(new EnumToStringConverter<PaymentStatus>());

            builder.Property(p => p.Method).IsRequired().HasConversion(new EnumToStringConverter<PaymentMethod>());
           
        }
    }
}
