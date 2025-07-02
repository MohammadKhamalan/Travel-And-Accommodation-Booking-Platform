using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;

public class CanBookRoomQuery : IRequest<bool>
{
    public Guid RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public CanBookRoomQuery(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        RoomId = roomId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }
}
