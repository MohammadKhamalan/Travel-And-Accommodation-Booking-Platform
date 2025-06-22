namespace TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

public record OwnerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string PhoneNumber
);
