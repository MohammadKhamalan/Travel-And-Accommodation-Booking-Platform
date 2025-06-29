using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class LoginUserQuery : IRequest<string>
{
    public LoginUserDto Dto { get; set; }
}
