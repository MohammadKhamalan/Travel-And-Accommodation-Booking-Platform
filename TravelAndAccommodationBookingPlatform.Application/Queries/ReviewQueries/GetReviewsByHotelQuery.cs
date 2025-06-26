using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;

public class GetReviewsByHotelQuery : IRequest<PaginatedList<ReviewDto>>
{
    public Guid HotelId { get; set; }
    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
