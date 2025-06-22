namespace TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    public float Rating { get; set; }
}
