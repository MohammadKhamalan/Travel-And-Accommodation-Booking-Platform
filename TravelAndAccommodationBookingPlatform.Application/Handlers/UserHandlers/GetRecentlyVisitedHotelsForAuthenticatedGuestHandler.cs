
using MediatR;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetRecentlyVisitedHotelsForAuthenticatedGuestHandler : IRequestHandler<GetRecentlyVisitedHotelsForAuthenticatedGuestQuery, List<Hotel>>
{
    private readonly IUserRepository _userRepository;

    public GetRecentlyVisitedHotelsForAuthenticatedGuestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Hotel>> Handle(GetRecentlyVisitedHotelsForAuthenticatedGuestQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetRecentlyVisitedHotelsForAuthenticatedGuestAsync(request.Email, request.Count);
    }
}
