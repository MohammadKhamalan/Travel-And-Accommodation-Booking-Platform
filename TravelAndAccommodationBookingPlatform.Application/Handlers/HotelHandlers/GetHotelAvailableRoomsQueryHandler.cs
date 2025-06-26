using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class GetHotelAvailableRoomsQueryHandler : IRequestHandler<GetHotelAvailableRoomsQuery, List<Room>>
{
    private readonly IHotelRepository _repository;

    public GetHotelAvailableRoomsQueryHandler(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Room>> Handle(GetHotelAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetHotelAvailableRoomsAsync(request.HotelId, request.CheckInDate, request.CheckOutDate);
    }
}