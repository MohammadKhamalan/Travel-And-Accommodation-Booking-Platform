
using MediatR;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetRecentlyVisitedHotelsForGuestHandler : IRequestHandler<GetRecentlyVisitedHotelsForGuestQuery, List<Hotel>>
{
    private readonly IUserRepository _userRepository;

    public GetRecentlyVisitedHotelsForGuestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Hotel>> Handle(GetRecentlyVisitedHotelsForGuestQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetRecentlyVisitedHotelsForGuestAsync(request.GuestId, request.Count);
    }
}
