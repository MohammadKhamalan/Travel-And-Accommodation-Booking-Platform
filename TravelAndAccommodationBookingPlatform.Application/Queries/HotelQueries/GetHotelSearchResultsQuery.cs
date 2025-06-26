using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetHotelSearchResultsQuery : IRequest<PaginatedList<HotelSearchResultDto>>
    {
        public HotelSearchParameters Parameters { get; set; }
    }
}
