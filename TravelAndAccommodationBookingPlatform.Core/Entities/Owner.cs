using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Owner : Person
    {
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
