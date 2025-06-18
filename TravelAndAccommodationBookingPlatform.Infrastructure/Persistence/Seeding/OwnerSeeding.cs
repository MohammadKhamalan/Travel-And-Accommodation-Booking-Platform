using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding
{
    public class OwnerSeeding
    {
        public static IEnumerable<Owner> GetSeedOwners()
        {
            return new List<Owner>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FirstName = "Waddah",
                LastName = "Khamalan",
                Email = "waddah.khamalan@hotels.com",
                DateOfBirth = DateTime.Parse("1960-5-10"),
                PhoneNumber = "+972598500504"
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FirstName = "Othman",
                LastName = "shabaro",
                Email = "othman.shabaro@hotels.com",
                DateOfBirth = DateTime.Parse("1975-8-15"),
                PhoneNumber = "+972598400403"
            },
            new()
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                FirstName = "salah",
                LastName = "khamalan",
                Email = "salah.khamalan@hotels.com",
                DateOfBirth = DateTime.Parse("1955-3-22"),
                PhoneNumber = "+972599600606"
            }
        };
        }
    }
}
