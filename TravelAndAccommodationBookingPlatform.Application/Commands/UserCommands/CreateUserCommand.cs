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
    public CreateUserDto Dto { get; set; }

}

