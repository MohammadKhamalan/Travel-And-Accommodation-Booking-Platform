using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Utils;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.UserHandlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      
        var hashedPassword = PasswordHasher.HashPassword(request.Dto.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            Email = request.Dto.Email,
            DateOfBirth = request.Dto.DateOfBirth,
            PhoneNumber = request.Dto.PhoneNumber,
            Password = hashedPassword, 
            Role = request.Dto.Role
        };

        await _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();
        return user.Id;
    }
}
