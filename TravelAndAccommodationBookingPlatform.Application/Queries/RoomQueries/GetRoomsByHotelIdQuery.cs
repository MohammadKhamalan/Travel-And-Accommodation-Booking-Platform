using MediatR;
using System;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;

public class GetRoomsByHotelIdQuery : IRequest<PaginatedList<RoomDto>>
{
    public Guid HotelId { get; set; }
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
