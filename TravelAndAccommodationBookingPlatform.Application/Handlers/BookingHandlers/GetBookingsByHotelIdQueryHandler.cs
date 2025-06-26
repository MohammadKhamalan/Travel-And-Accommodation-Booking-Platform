using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;

public class GetBookingsByHotelIdQueryHandler : IRequestHandler<GetBookingsByHotelIdQuery, PaginatedList<BookingResponseDto>>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;

    public GetBookingsByHotelIdQueryHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BookingResponseDto>> Handle(GetBookingsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _repository.GetAllByHotelIdAsync(request.HotelId, request.PageNumber, request.PageSize);
        return new PaginatedList<BookingResponseDto>(
            _mapper.Map<List<BookingResponseDto>>(bookings.Items),
            bookings.PageData.TotalItemCount,
            bookings.PageData.PageSize,
            bookings.PageData.CurrentPage
        );
    }
}
