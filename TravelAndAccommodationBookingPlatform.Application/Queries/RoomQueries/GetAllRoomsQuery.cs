using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;

public class GetAllRoomsQuery : IRequest<PaginatedList<RoomDto>>
{
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
