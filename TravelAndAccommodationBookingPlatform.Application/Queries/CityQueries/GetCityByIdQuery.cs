using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;

public class GetCityByIdQuery : IRequest<CityDto>
{
    public Guid Id { get; set; }
    public bool IncludeHotels { get; set; }
}
