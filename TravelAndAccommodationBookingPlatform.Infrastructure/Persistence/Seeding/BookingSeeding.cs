using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;


public class BookingSeeding
{
    public static IEnumerable<Booking> GetSeedBookings()
    {
        return new List<Booking>
        {
            new()
            {
                Id = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                RoomId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef123456a"),
                CheckInDate = DateTime.Parse("2025-07-01"),
                CheckOutDate = DateTime.Parse("2025-07-09"),
                BookingDate = DateTime.Parse("2025-06-18"),
                Price = 2250.00
            },
            new()
            {
                Id = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                RoomId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 
                UserId = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef123456a"), 
                CheckInDate = DateTime.Parse("2025-07-15"),
                CheckOutDate = DateTime.Parse("2025-07-20"),
                BookingDate = DateTime.Parse("2025-06-18"),
                Price = 1500.00
            },
            new()
            {
                Id = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                RoomId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), 
                UserId = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"),
                CheckInDate =  DateTime.Parse("2025-07-02"),
                CheckOutDate = DateTime.Parse("2025-07-06"),
                BookingDate = DateTime.Parse("2025-06-18"),
                Price = 750.00 
            }
        };
    }
}