using MediatR;
using System;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries
{
    public class CheckRoomBelongsToHotelQuery : IRequest<bool>
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
    }
}
