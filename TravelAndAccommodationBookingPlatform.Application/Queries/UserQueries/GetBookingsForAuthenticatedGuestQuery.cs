using MediatR;
using System;
using System.Collections.Generic;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

public class GetBookingsForAuthenticatedGuestQuery : IRequest<List<Booking>>
{
    public string Email { get; }
    public int Count { get; }

   
}
