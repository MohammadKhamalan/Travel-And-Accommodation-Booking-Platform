using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.ReviewHandlers;
public class GetReviewByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedReviewDto()
    {
        var repoMock = new Mock<IReviewRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetReviewByIdQueryHandler(repoMock.Object, mapperMock.Object);

        var id = Guid.NewGuid();
        var review = new Review { Id = id };
        var dto = new ReviewDto { Id = id };

        repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(review);
        mapperMock.Setup(m => m.Map<ReviewDto>(review)).Returns(dto);

        var result = await handler.Handle(new GetReviewByIdQuery { ReviewId = id }, CancellationToken.None);

        result.Should().BeEquivalentTo(dto);
    }
}