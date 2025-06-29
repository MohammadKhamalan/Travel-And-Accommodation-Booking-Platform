using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

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
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            Email = request.Dto.Email,
            DateOfBirth = request.Dto.DateOfBirth,
            PhoneNumber = request.Dto.PhoneNumber,
            Password = request.Dto.Password,
            Role = request.Dto.Role
        };

        await _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();
        return user.Id;
    }

}
