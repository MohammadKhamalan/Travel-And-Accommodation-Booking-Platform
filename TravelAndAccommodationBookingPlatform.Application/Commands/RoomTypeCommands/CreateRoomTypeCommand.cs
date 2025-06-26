using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomTypeCommands;

public class CreateRoomTypeCommand : IRequest<Guid>
{
    public CreateRoomTypeDto RoomType { get; set; }
}
