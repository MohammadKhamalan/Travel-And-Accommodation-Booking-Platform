using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;

public class GetRoomByIdQuery : IRequest<RoomDto>
{
    public Guid RoomId { get; set; }
}
