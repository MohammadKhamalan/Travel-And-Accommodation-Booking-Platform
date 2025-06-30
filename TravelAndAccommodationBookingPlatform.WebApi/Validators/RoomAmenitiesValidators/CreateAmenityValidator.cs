using FluentValidation;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;

namespace TravelAndAccommodationBookingPlatform.WebApi.Validators.RoomAmenitiesValidators;


    public class CreateAmenityValidator : AbstractValidator<CreateAmenityDto>
    {
        public CreateAmenityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required.")
                .MaximumLength(100).WithMessage("Amenity name must be at most 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Amenity description is required.")
                .MaximumLength(250).WithMessage("Amenity description must be at most 250 characters.");
        }
    }

