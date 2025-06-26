using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;
public class GetGuestIdByEmailQuery : IRequest<Guid>
{
    public string Email { get; }
}