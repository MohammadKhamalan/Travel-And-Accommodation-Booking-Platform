using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
public class GetDiscountsByRoomTypeQuery : IRequest<PaginatedList<DiscountDto>>
{
    public Guid RoomTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}