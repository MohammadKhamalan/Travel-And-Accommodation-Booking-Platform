
namespace TravelAndAccommodationBookingPlatform.Core.Entities;
public class PendingBooking
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Price { get; set; }

    public Room Room { get; set; }
    public User User { get; set; }
}

