using MediatR;
using System.Collections.Generic;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetFeaturedDealsQuery : IRequest<List<FeaturedHotelDto>>
    {
        public int Count { get; set; }
    }
}
