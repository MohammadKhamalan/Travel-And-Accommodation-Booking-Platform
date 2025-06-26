using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;

public class GetOwnerByIdQuery : IRequest<OwnerDto>
{
    public Guid Id { get; set; }
}
