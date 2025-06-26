using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;

public class CheckBookingExistenceForGuestQueryHandler : IRequestHandler<CheckBookingExistenceForGuestQuery, bool>
{
    private readonly IBookingRepository _repository;

    public CheckBookingExistenceForGuestQueryHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CheckBookingExistenceForGuestQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CheckBookingExistenceForGuestAsync(request.BookingId, request.GuestEmail);
    }
}
