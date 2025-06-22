using System;
using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

public class CreateUserDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }

    [Required]
    public UserRole Role { get; set; }
}
