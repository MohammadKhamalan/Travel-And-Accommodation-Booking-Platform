using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;

public class CreateDiscountCommand : IRequest<Guid>
{
    public Guid RoomTypeId { get; set; }
    public float DiscountPercentage { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
