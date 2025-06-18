using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class AmenitySeeding
{
    public static IEnumerable<Amenity> GetSeedAmenities()
    {
        return new List<Amenity>
        {
            new()
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                Name = "Swimming Pool",
                Description = "Outdoor temperature-controlled pool"
            },
            new()
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                Name = "Spa",
                Description = "Full-service spa with massage treatments"
            },
            new()
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                Name = "Business Center",
                Description = "24/7 business facilities with printing"
            }
        };
    }
}
