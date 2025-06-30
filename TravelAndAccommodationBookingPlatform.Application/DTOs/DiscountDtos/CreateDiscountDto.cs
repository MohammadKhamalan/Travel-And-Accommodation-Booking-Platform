using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

public class CreateDiscountDto
{
   
    public Guid RoomTypeId { get; set; }
  
    public float DiscountPercentage { get; set; }
   
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
