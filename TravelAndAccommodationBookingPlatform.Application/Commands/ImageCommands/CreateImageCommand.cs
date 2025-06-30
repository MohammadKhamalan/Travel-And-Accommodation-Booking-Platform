using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ImageCommands;

public class CreateImageCommand : IRequest<Guid>
{
    public string Url { get; set; }
    public string Type { get; set; }
    public string Format { get; set; }
    public Guid? HotelId { get; set; }
    public Guid? CityId { get; set; }
    public string OwnerType { get; set; }
}
