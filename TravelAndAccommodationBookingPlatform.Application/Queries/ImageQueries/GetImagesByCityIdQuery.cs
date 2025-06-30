using MediatR;
using System;
using System.Collections.Generic;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries
{
    public class GetImagesByCityIdQuery : IRequest<List<ImageDto>>
    {
        public GetImagesByCityIdQuery(Guid cityId)
        {
            CityId = cityId;
        }

        public Guid CityId { get; set; }
    }
}
