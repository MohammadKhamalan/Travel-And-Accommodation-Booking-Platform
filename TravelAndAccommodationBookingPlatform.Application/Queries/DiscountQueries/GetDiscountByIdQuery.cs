using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;

public class GetDiscountByIdQuery : IRequest<DiscountDto?>
{
    public Guid Id { get; set; }
}
