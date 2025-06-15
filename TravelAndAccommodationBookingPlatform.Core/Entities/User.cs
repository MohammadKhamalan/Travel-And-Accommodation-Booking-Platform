using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class User : Person
{
    public string Password { get; set; }
   public UserRole Role { get; set; }
    public IList<Booking> Bookings { get; set; }

}
