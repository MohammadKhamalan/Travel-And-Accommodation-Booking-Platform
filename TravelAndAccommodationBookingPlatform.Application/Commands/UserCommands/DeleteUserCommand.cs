﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

