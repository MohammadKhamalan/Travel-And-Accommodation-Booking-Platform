using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public Guid CityId { get; set; }
        public Guid OwnerId { get; set; }
        public float Rating { get; set; }
        public string StreetAddress { get; set; }
        public int NumberOfRooms { get; set; }
        public string PhoneNumber { get; set; }

        public City City { get; set; }
        public Owner Owner { get; set; }

        public List<Image> Images { get; set; }
    }
}
