using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;

public class CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int Role { get; set; } 
}

