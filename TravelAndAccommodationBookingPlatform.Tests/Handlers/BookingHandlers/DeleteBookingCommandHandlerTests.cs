using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.BookingTest
{
    public class DeleteBookingCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly DeleteBookingCommandHandler _handler;

        public DeleteBookingCommandHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _handler = new DeleteBookingCommandHandler(_bookingRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteBooking_WhenBookingExists()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var command = new DeleteBookingCommand { BookingId = bookingId };

            _bookingRepositoryMock.Setup(r => r.DeleteAsync(bookingId)).Returns(Task.CompletedTask);
            _bookingRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            _bookingRepositoryMock.Verify(r => r.DeleteAsync(bookingId), Times.Once);
            _bookingRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBookingDoesNotExist()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var command = new DeleteBookingCommand { BookingId = bookingId };

            _bookingRepositoryMock
                .Setup(r => r.DeleteAsync(bookingId))
                .ThrowsAsync(new NotFoundException("Booking not found."));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Booking not found.");
        }
    }
}
