using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;

public class RoomAmenityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    
    public List<Guid> RoomTypeIds { get; set; } = new();
}
