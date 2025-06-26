using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;

public class UpdateRoomCommand : IRequest<RoomDto>
{
    public Guid Id { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public float Rating { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
