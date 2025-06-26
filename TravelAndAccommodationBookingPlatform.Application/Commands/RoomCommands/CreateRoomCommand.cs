using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;

public class CreateRoomCommand : IRequest<Guid>
{
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public float Rating { get; set; }
}
