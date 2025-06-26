using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
public class HasOverlappingDiscountQuery : IRequest<bool>
{
    public Guid RoomTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}