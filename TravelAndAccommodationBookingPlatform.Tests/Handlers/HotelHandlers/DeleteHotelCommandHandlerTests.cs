using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using Xunit;
using MediatR;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.HotelTest
{
    public class DeleteHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly DeleteHotelCommandHandler _handler;

        public DeleteHotelCommandHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new DeleteHotelCommandHandler(_hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteHotel_WhenIdIsValid()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var command = new DeleteHotelCommand { Id = hotelId };

            _hotelRepositoryMock.Setup(r => r.DeleteAsync(hotelId)).Returns(Task.CompletedTask);
            _hotelRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);


            // Assert
            Assert.Equal(Unit.Value, result);
            _hotelRepositoryMock.Verify(r => r.DeleteAsync(hotelId), Times.Once);
            _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var command = new DeleteHotelCommand { Id = hotelId };

            _hotelRepositoryMock.Setup(r => r.DeleteAsync(hotelId)).ThrowsAsync(new InvalidOperationException("Hotel not found"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            _hotelRepositoryMock.Verify(r => r.DeleteAsync(hotelId), Times.Once);
            _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
