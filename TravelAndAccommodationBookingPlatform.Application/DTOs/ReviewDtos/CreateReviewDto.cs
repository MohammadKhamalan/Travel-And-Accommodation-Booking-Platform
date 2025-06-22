using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;

public class CreateReviewDto
{
    [Required]
    public Guid BookingId { get; set; }
    public string Comment { get; set; }
    [Required]
    public float Rating { get; set; }
}
