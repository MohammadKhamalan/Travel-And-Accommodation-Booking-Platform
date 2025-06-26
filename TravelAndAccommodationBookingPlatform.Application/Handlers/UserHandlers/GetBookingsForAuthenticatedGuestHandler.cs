
using MediatR;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetBookingsForAuthenticatedGuestHandler : IRequestHandler<GetBookingsForAuthenticatedGuestQuery, List<Booking>>
{
    private readonly IUserRepository _userRepository;

    public GetBookingsForAuthenticatedGuestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Booking>> Handle(GetBookingsForAuthenticatedGuestQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetBookingsForAuthenticatedGuestAsync(request.Email, request.Count);
    }
}
