using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries;

public class GetImageByIdQuery : IRequest<ImageDto>
{
    public GetImageByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
