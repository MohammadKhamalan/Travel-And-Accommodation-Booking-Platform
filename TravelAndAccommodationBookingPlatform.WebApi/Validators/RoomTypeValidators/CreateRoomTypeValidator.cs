using FluentValidation;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebApi.Validators.RoomTypeValidators;

public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeDto>
{
    public CreateRoomTypeValidator()
    {
        RuleFor(x => x.RoomCategory)
    .Must(value => Enum.TryParse<RoomCategory>(value, true, out _))
    .WithMessage("Invalid room category. Valid values are: Single, Double, Suite, Family.");


        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be greater than zero.");

        RuleFor(x => x.AmenityIds)
            .NotNull().WithMessage("Amenity list cannot be null.");
    }
}
