using MediatR;
using System;
using System.Collections.Generic;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries
{
    public class GetImagesByHotelIdQuery : IRequest<List<ImageDto>>
    {
        public GetImagesByHotelIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; set; }
    }
}
