using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;

public class DeleteRoomCommand : IRequest<Unit>
{
    public Guid RoomId { get; set; }
}
