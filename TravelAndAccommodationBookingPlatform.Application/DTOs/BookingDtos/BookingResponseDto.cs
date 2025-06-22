namespace TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

public class BookingResponseDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public string RoomType { get; set; }
    public Guid UserId { get; set; }
    public string GuestFullName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime BookingDate { get; set; }
    public double Price { get; set; }
}
