using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, OwnerDto>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public UpdateOwnerCommandHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OwnerDto> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        var updatedOwner = new Owner
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber
        };
        await _repository.UpdateAsync(updatedOwner);
        await _repository.SaveChangesAsync();
        return _mapper.Map<OwnerDto>(updatedOwner);
    }
}
