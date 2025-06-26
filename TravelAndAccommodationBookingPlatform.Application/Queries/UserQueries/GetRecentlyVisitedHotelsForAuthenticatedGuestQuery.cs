using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;
public class GetRecentlyVisitedHotelsForAuthenticatedGuestQuery : IRequest<List<Hotel>>
{
    public string Email { get; }
    public int Count { get; }
}