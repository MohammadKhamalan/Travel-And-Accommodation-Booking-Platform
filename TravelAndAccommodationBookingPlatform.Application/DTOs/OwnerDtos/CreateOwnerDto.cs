using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

public class CreateOwnerDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
}
