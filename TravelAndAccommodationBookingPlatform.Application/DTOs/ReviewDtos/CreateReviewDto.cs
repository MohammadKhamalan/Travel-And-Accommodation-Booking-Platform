using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;

public class CreateReviewDto
{
    
    public Guid BookingId { get; set; }
    public string Comment { get; set; }    
    public float Rating { get; set; }
}
