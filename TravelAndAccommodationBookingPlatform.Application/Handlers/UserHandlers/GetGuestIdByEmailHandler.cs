
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetGuestIdByEmailHandler : IRequestHandler<GetGuestIdByEmailQuery, Guid>
{
    private readonly IUserRepository _userRepository;

    public GetGuestIdByEmailHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(GetGuestIdByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetGuestIdByEmailAsync(request.Email);
    }
}
