﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Exceptions;

public class DataConstraintViolationException : Exception
{
    public DataConstraintViolationException(string message) : base(message)
    {

    }
}
