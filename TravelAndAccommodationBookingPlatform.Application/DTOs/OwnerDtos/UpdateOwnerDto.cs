using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

public class UpdateOwnerDto
{
   
    public Guid Id { get; set; }


    public string FirstName { get; set; } = null!;

 
    public string LastName { get; set; } = null!;


    public string Email { get; set; } = null!;


    public DateTime DateOfBirth { get; set; }

 
  
    public string PhoneNumber { get; set; } = null!;
}
