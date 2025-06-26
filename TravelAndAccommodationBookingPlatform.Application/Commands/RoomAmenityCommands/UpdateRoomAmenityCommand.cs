using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;

public class UpdateRoomAmenityCommand : IRequest<RoomAmenityDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
