using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.DiscountHandlers;

public class GetDiscountByIdQueryHandlerTests
{

    [Fact]
    public async Task Handle_ShouldReturnDiscountDto()
    {
        var repoMock = new Mock<IDiscountRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetDiscountByIdQueryHandler(repoMock.Object, mapperMock.Object);

        var discount = new Discount { Id = Guid.NewGuid() };
        var dto = new DiscountDto { Id = discount.Id };

        repoMock.Setup(r => r.GetByIdAsync(discount.Id)).ReturnsAsync(discount);
        mapperMock.Setup(m => m.Map<DiscountDto>(discount)).Returns(dto);

        var result = await handler.Handle(new GetDiscountByIdQuery { Id = discount.Id }, CancellationToken.None);

        result.Should().BeEquivalentTo(dto);
    }
}
