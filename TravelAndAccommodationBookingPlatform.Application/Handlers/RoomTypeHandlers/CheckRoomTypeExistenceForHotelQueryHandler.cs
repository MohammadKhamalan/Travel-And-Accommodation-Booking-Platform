using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomTypeHandlers;

public class CheckRoomTypeExistenceForHotelQueryHandler : IRequestHandler<CheckRoomTypeExistenceForHotelQuery, bool>
{
    private readonly IRoomTypeRepository _repository;

    public CheckRoomTypeExistenceForHotelQueryHandler(IRoomTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CheckRoomTypeExistenceForHotelQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CheckRoomTypeExistenceForHotel(request.HotelId, request.RoomTypeId);
    }
}
