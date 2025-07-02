using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using Xunit;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.DiscountHandlers;

public class GetDiscountsByRoomTypeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedDiscounts()
    {
        var repoMock = new Mock<IDiscountRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetDiscountsByRoomTypeQueryHandler(repoMock.Object, mapperMock.Object);

        var roomTypeId = Guid.NewGuid();
        var discountList = new List<Discount> { new Discount { Id = Guid.NewGuid() } };
        var paged = new PaginatedList<Discount>(discountList, 1, 10, 1);

        repoMock.Setup(r => r.GetAllByRoomTypeIdAsync(roomTypeId, 1, 10)).ReturnsAsync(paged);
        mapperMock.Setup(m => m.Map<List<DiscountDto>>(discountList)).Returns(new List<DiscountDto>());

        var result = await handler.Handle(new GetDiscountsByRoomTypeQuery
        {
            RoomTypeId = roomTypeId,
            PageNumber = 1,
            PageSize = 10
        }, CancellationToken.None);

        result.Items.Should().NotBeNull();
    }
}
