using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room{get; set;}
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime BookingDate { get; set; }
    public double Price { get; set; }
    public Review? Review { get; set; }
    public Payment? Payment { get; set; }
}
