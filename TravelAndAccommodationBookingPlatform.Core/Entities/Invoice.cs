using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class Invoice
{
        public Guid Id { get; set; }
        public DateTime BookingDate { get; set; }
        public double Price { get; set; }
        public string HotelName { get; set; }
        public string OwnerName { get; set; }
    
}
