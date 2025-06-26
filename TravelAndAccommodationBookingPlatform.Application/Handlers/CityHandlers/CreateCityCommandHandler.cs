using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Guid>
{
    private readonly ICityRepository _repository;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(ICityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = new City
        {
            Name = request.Name,
            CountryName = request.CountryName,
            PostOffice = request.PostOffice
        };

        await _repository.InsertAsync(city);
        await _repository.SaveChangesAsync();
        return city.Id;
    }
}
