using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetAllUsersQuery : IRequest<List<UserDto>> { }

