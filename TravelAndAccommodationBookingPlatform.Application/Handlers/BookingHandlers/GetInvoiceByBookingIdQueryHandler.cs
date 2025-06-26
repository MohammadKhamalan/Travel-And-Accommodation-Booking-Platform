using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetInvoiceByBookingIdQueryHandler : IRequestHandler<GetInvoiceByBookingIdQuery, InvoiceDto?>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;

    public GetInvoiceByBookingIdQueryHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InvoiceDto?> Handle(GetInvoiceByBookingIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetInvoiceByBookingIdAsync(request.BookingId);
        return invoice is null ? null : _mapper.Map<InvoiceDto>(invoice);
    }
}
