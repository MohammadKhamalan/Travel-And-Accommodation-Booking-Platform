using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingResponseDto?>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;

    public GetBookingByIdQueryHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookingResponseDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.BookingId);
        return booking is null ? null : _mapper.Map<BookingResponseDto>(booking);
    }
}
