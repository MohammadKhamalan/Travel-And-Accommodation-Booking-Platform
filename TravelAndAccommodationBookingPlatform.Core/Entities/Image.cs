using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class Image
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public ImageType Type { get; set; }
    public ImageFormat Format { get; set; }
    public Guid? HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public Guid? CityId { get; set; }
    public City City { get; set; }
    public ImageOwnerType OwnerType { get; set; }
}
