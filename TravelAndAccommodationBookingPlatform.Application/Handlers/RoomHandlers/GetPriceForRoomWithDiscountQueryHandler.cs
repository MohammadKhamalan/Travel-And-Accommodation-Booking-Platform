using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers
{
    public class GetPriceForRoomWithDiscountQueryHandler : IRequestHandler<GetPriceForRoomWithDiscountQuery, float>
    {
        private readonly IRoomRepository _repository;

        public GetPriceForRoomWithDiscountQueryHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<float> Handle(GetPriceForRoomWithDiscountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPriceForRoomWithDiscount(request.RoomId);
        }
    }
}
