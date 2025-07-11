﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities;

public class Discount
{
    public Guid Id { get; set; }
    public Guid RoomTypeId { get; set; }  
    public RoomType RoomType { get; set; }  
    public float DiscountPercentage { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
