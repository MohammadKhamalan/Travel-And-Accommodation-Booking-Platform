using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers;

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Guid>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public CreateOwnerCommandHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = new Owner
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber
        };

        await _repository.InsertAsync(owner);
        await _repository.SaveChangesAsync();
        return owner.Id;
    }
}
