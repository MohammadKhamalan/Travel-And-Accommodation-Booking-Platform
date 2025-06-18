using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class DiscountSeeding
{
    public static IEnumerable<Discount> GetSeedDiscounts()
    {
        return new List<Discount>
        {
            new()
            {
                Id = Guid.Parse("22222222-0000-0000-0000-000000000001"),
                RoomTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
                DiscountPercentage = 15.0f,
                FromDate = DateTime.Parse("2025-06-20"),
                ToDate =  DateTime.Parse("2025-07-01")
            },
            new()
            {
                Id = Guid.Parse("22222222-0000-0000-0000-000000000002"),
                RoomTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 
                DiscountPercentage = 20.0f,
                FromDate =  DateTime.Parse("2025-06-19"),
                ToDate = DateTime.Parse("2025-07-01")
            },
            new()
            {
                Id = Guid.Parse("22222222-0000-0000-0000-000000000003"),
                RoomTypeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), 
                DiscountPercentage = 10.0f,
                FromDate = DateTime.Parse("2025-06-20"),
                ToDate = DateTime.Parse("2025-07-01")
            }
        };
    }
}
