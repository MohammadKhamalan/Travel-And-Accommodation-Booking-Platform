﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class City
{
   
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOffice { get; set; }
    public IList<Hotel> Hotels { get; set; }
    public IList<Image> Images { get; set; }
}
