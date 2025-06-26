using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityDto>
{
    private readonly ICityRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCityCommandHandler(ICityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CityDto> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = new City
        {
            Id = request.Id,
            Name = request.Name,
            CountryName = request.CountryName,
            PostOffice = request.PostOffice
        };

        await _repository.UpdateAsync(city);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CityDto>(city);
    }
}
