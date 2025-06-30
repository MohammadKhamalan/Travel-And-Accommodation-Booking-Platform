using FluentValidation;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

namespace TravelAndAccommodationBookingPlatform.WebApi.Validators.ImageValidators
{
    public class CreateImageRequestValidator : AbstractValidator<CreateImageRequestDto>
    {
        public CreateImageRequestValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("Image URL is required.")
                               .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Invalid image URL format.");

            RuleFor(x => x.Type).NotEmpty().WithMessage("Image type is required.");

            RuleFor(x => x.Format).NotEmpty().WithMessage("Image format is required.");

            RuleFor(x => x.OwnerType).NotEmpty().WithMessage("OwnerType is required.");
        }
    }
}
