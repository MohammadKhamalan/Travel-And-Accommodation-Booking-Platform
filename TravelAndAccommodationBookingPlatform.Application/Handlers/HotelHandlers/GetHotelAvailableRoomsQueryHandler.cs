using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;

public class GetHotelAvailableRoomsQueryHandler : IRequestHandler<GetHotelAvailableRoomsQuery, List<Room>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelAvailableRoomsQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<List<Room>> Handle(GetHotelAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _hotelRepository.GetHotelAvailableRoomsAsync(
            request.HotelId, request.CheckInDate, request.CheckOutDate
        );

        var result = rooms.Select(r => new Room
        {
            Id = r.Id,
            AdultsCapacity = r.AdultsCapacity,
            ChildrenCapacity = r.ChildrenCapacity,
            Rating = r.Rating,
            RoomTypeId = r.RoomTypeId,
            HotelId = r.HotelId,
            RoomType = r.RoomType == null ? null : new RoomType
            {
                RoomTypeId = r.RoomType.RoomTypeId,
                RoomCategory = r.RoomType.RoomCategory,
                PricePerNight = r.RoomType.PricePerNight
               
            },
            Hotel = null, 
            Bookings = new List<Booking>(), 
            CreatedAt = r.CreatedAt,
            ModifiedAt = r.ModifiedAt
        }).ToList();

        return result;
    }

}
