using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;
using TravelAndAccommodationBookingPlatform.Application.Utils; 
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Utils;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.UserHandlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;

    public LoginUserHandler(
         IUserRepository userRepository,
         ITokenService tokenService)
    {
        _repository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(request.Dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

       
        bool isPasswordValid = PasswordHasher.VerifyPassword(request.Dto.Password, user.Password);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _tokenService.GenerateToken(user);
    }
}
