using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .Property(review => review.Comment)
            .HasMaxLength(300);

        builder
            .Property(review => review.Rating)
            .IsRequired();
      

        builder.HasCheckConstraint("CK_Review_RatingRange", "[Rating] >= 0 AND [Rating] <= 5");

      
        builder.ToTable("Reviews");
    }
}
