using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.UserHandlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;

    public LoginUserHandler(IUserRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(request.Dto.Email);
        if (user == null || user.Password != request.Dto.Password)
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _tokenService.GenerateToken(user);
    }
}
