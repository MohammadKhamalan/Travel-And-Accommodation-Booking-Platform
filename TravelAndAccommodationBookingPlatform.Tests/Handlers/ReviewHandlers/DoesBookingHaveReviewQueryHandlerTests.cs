using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.ReviewHandlers;
public class DoesBookingHaveReviewQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTrue_IfReviewExists()
    {
        var repoMock = new Mock<IReviewRepository>();
        var handler = new DoesBookingHaveReviewQueryHandler(repoMock.Object);
        var bookingId = Guid.NewGuid();

        repoMock.Setup(r => r.DoesBookingHaveReviewAsync(bookingId)).ReturnsAsync(true);

        var result = await handler.Handle(new DoesBookingHaveReviewQuery { BookingId = bookingId }, CancellationToken.None);

        result.Should().BeTrue();
    }
}