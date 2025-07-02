using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.ReviewHandlers;
public class GetReviewsByHotelIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedReviewDtos()
    {
        var repoMock = new Mock<IReviewRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetReviewsByHotelIdQueryHandler(repoMock.Object, mapperMock.Object);

        var hotelId = Guid.NewGuid();
        var reviewList = new List<Review> { new Review() };
        var reviewDtoList = new List<ReviewDto> { new ReviewDto() };
        var pagedReviews = new PaginatedList<Review>(reviewList, 1, 10, 1);

        repoMock.Setup(r => r.GetAllByHotelIdAsync(hotelId, null, 1, 10)).ReturnsAsync(pagedReviews);
        mapperMock.Setup(m => m.Map<List<ReviewDto>>(reviewList)).Returns(reviewDtoList);

        var result = await handler.Handle(new GetReviewsByHotelQuery
        {
            HotelId = hotelId,
            PageNumber = 1,
            PageSize = 10
        }, CancellationToken.None);

        result.Items.Should().HaveCount(1);
    }
}