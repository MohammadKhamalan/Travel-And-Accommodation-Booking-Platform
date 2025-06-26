using MediatR;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;

public class GetHotelAvailableRoomsQuery : IRequest<List<Room>>
{
    public Guid HotelId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

   
}
