using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class ReviewSeeding
{
    public static IEnumerable<Review> GetSeedReviews()
    {
        return new List<Review>
        {
            new()
            {
                Id = Guid.Parse("44444444-0000-0000-0000-000000000001"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                Comment = "Absolutely stunning hotel with exceptional service",
                Rating = 5.0f,
                ReviewDate = DateTime.Parse("2025-05-01")  
            },
            new()
            {
                Id = Guid.Parse("44444444-0000-0000-0000-000000000002"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                Comment = "Great location but rooms need updating",
                Rating = 3.5f,
                ReviewDate = DateTime.Parse("2025-05-15")
            },
            new()
            {
                Id = Guid.Parse("44444444-0000-0000-0000-000000000003"),
                BookingId = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                Comment = "Good value for money",
                Rating = 4.0f,
                ReviewDate = DateTime.Parse("2025-04-22")
            }
        };
    }
}
