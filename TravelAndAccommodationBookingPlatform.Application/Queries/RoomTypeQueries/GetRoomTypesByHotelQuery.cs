using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;

public class GetRoomTypesByHotelQuery : IRequest<PaginatedList<RoomTypeDto>>
{
    public Guid HotelId { get; set; }
    public bool IncludeAmenities { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
