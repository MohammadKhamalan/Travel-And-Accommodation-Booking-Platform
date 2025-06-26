using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries
{
    public class GetPriceForRoomWithDiscountQuery : IRequest<float>
    {
        public Guid RoomId { get; set; }
    }
}
