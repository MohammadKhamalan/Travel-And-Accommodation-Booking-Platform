using FluentValidation;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;

namespace TravelAndAccommodationBookingPlatform.API.Validators.RoomValidators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("RoomTypeId is required.");

            RuleFor(x => x.AdultsCapacity)
                .GreaterThanOrEqualTo(1).WithMessage("AdultsCapacity must be at least 1.");

            RuleFor(x => x.ChildrenCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("ChildrenCapacity must be 0 or more.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
        }
    }
}
