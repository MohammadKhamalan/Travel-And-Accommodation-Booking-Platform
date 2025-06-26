using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomAmenityQueries;

public class GetAllRoomAmenitiesQuery : IRequest<PaginatedList<RoomAmenityDto>>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
