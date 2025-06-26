using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;

public class DeleteRoomAmenityCommand : IRequest<Unit>
{
    public Guid AmenityId { get; set; }
}
