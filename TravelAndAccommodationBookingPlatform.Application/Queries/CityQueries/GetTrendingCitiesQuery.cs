using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;

public class GetTrendingCitiesQuery : IRequest<List<TrendingCityResponseDto>>
{
    public int Count { get; set; }
}
