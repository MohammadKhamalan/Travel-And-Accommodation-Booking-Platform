using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;

public class CheckRoomTypeExistenceForHotelQuery : IRequest<bool>
{
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
}
