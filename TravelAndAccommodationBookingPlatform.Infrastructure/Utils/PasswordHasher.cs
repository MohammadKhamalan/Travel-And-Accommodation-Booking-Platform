using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using BCryptNet = BCrypt.Net.BCrypt;
namespace TravelAndAccommodationBookingPlatform.Infrastructure.Utils
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCryptNet.EnhancedHashPassword(password, HashType.SHA384);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.EnhancedVerify(password, hashedPassword, HashType.SHA384);
        }
    }
}
