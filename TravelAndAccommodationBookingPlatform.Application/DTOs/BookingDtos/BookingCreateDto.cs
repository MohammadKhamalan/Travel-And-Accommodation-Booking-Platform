namespace TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

public class BookingCreateDto
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}
