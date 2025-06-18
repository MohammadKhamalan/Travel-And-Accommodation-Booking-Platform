using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Infrastructure.Utils;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Seeding;

public class UserSeeding
{
    public static IEnumerable<User> GetSeedUsers()
    {
        return new List<User>
            {
               
                new()
                {
                    Id = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"),
                    FirstName = "Hiba",
                    LastName = "Al-kurd",
                    Email = "hiba.alkurd@gmail.com",
                    Password = PasswordHasher.HashPassword("Admin@1234"),
                    Role = UserRole.Admin,
                    DateOfBirth = DateTime.Parse("1996-01-01"),
                    PhoneNumber = "+972568543234"
                },
                
               
                new()
                {
                    Id = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef123456a"),
                    FirstName = "Mohammad",
                    LastName = "Khamalan",
                    Email = "mohammad.khamalan@example.com",
                    Password = PasswordHasher.HashPassword("Guest@1234"),
                    Role = UserRole.Guest,
                    DateOfBirth = DateTime.Parse("2002-03-29"),
                    PhoneNumber = "+972598168640"
                },
                
                
                new()
                {
                    Id = Guid.Parse("c3d4e5f6-3456-7890-1234-def123456abc"),
                    FirstName = "Sarah",
                    LastName = "Wilson",
                    Email = "s.wilson@hotelowner.com",
                    Password = PasswordHasher.HashPassword("Guest@1234"),
                    Role = UserRole.Admin,
                    DateOfBirth = DateTime.Parse("1985-08-20"),
                    PhoneNumber = "+972592345654"
                }
            };
    }
}

