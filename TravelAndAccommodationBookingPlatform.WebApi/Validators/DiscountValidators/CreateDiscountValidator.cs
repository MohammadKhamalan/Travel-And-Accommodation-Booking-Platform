using FluentValidation;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.WebApi.Validators.DiscountValidators;

public class CreateDiscountValidator : AbstractValidator<CreateDiscountDto>
{
    public CreateDiscountValidator()
    {
        RuleFor(x => x.RoomTypeId)
            .NotEmpty().WithMessage("RoomTypeId is required.");

        RuleFor(x => x.DiscountPercentage)
            .GreaterThan(0).WithMessage("Discount must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100%.");

        RuleFor(x => x.FromDate)
            .NotEmpty().WithMessage("FromDate is required.")
            .LessThan(x => x.ToDate).WithMessage("FromDate must be before ToDate.");

        RuleFor(x => x.ToDate)
            .NotEmpty().WithMessage("ToDate is required.");
    }
}
