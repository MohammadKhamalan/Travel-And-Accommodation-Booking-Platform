using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

    public class Amenity
    {
 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<RoomType> RoomTypes { get; set; }

}

