
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

    public class RoomType
    {
        public Guid RoomTypeId { get; set; }
        public RoomType RoomCategory { get; set; }
        public decimal PricePerNight { get; set; }  

        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }

