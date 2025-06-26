using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;

public class UpdateOwnerCommand : IRequest<OwnerDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
}
