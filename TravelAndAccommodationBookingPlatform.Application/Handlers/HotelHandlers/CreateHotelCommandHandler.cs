﻿using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
{
    private readonly IHotelRepository _repository;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(IHotelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        bool isDuplicate = await _repository.IsHotelNameAndAddressDuplicateAsync(request.Name, request.StreetAddress);

        if (isDuplicate)
            throw new InvalidOperationException("A hotel with the same name and address already exists.");

        var hotel = _mapper.Map<Hotel>(request);
        await _repository.InsertAsync(hotel);
        await _repository.SaveChangesAsync();
        return hotel.Id;
    }

}