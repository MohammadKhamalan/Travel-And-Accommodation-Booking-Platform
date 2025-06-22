using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

public class CreateDiscountDto
{
    [Required]
    public Guid RoomTypeId { get; set; }
    [Required]
    public float DiscountPercentage { get; set; }
    [Required]
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
