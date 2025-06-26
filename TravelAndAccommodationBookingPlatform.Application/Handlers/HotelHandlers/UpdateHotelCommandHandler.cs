using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelDto>
{
    private readonly IHotelRepository _repository;
    private readonly IMapper _mapper;

    public UpdateHotelCommandHandler(IHotelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<HotelDto> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = _mapper.Map<Hotel>(request);
        await _repository.UpdateAsync(hotel);
        await _repository.SaveChangesAsync();
        return _mapper.Map<HotelDto>(hotel);
    }
}