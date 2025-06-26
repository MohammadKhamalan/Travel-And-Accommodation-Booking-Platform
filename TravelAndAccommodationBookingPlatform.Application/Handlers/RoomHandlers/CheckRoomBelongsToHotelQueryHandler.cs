using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers
{
    public class CheckRoomBelongsToHotelQueryHandler : IRequestHandler<CheckRoomBelongsToHotelQuery, bool>
    {
        private readonly IRoomRepository _repository;

        public CheckRoomBelongsToHotelQueryHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CheckRoomBelongsToHotelQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CheckRoomBelongsToHotelAsync(request.HotelId, request.RoomId);
        }
    }
}
