using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class CanBookRoomQueryHandler : IRequestHandler<CanBookRoomQuery, bool>
{
    private readonly IBookingRepository _repository;

    public CanBookRoomQueryHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CanBookRoomQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CanBookRoom(request.RoomId, request.CheckInDate, request.CheckOutDate);
    }
}
