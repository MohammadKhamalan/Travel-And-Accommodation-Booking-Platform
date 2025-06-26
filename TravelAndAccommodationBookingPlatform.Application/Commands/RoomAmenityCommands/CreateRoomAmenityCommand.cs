using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;

public class CreateRoomAmenityCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
