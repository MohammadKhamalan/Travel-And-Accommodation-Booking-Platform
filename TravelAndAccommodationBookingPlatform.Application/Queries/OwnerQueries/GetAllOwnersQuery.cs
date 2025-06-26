using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;

public class GetAllOwnersQuery : IRequest<List<OwnerDto>> { }

