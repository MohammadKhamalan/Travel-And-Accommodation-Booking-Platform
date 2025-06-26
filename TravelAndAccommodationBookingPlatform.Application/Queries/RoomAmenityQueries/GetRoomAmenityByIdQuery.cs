using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomAmenityQueries;

public class GetRoomAmenityByIdQuery : IRequest<RoomAmenityDto?>
{
    public Guid AmenityId { get; set; }
}
