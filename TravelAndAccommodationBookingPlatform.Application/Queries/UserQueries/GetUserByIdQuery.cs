using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }
}
