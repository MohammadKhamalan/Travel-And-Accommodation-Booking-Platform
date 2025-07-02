using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Tests.Handlers.BookingTest
{
    public class CreateBookingCommandHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly CreateBookingCommandHandler _handler;

        public CreateBookingCommandHandlerTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _handler = new CreateBookingCommandHandler(_bookingRepositoryMock.Object, _roomRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateBooking_WhenDataIsValid()
        {
            var roomId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var command = new CreateBookingCommand
            {
                RoomId = roomId,
                UserId = userId,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(3)
            };

            var room = new Room
            {
                Id = roomId,
                RoomType = new RoomType { PricePerNight = 100 }
            };

            var insertedBooking = new Booking();
            var expectedId = Guid.NewGuid();

            _roomRepositoryMock.Setup(r => r.GetByIdWithRoomTypeAsync(roomId))
                .ReturnsAsync(room);

            _bookingRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<Booking>()))
                .Callback<Booking>(b =>
                {
                    b.Id = expectedId;
                    insertedBooking = b;
                })
                .ReturnsAsync(() => insertedBooking);

            _bookingRepositoryMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(expectedId);
            insertedBooking.Price.Should().Be(300);
            _bookingRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<Booking>()), Times.Once);
            _bookingRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenRoomIsNull()
        {
            var command = new CreateBookingCommand
            {
                RoomId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2)
            };

            _roomRepositoryMock.Setup(r => r.GetByIdWithRoomTypeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Room)null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Room or RoomType not found.");
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenRoomTypeIsNull()
        {
            var command = new CreateBookingCommand
            {
                RoomId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };

            var room = new Room
            {
                Id = command.RoomId,
                RoomType = null
            };

            _roomRepositoryMock.Setup(r => r.GetByIdWithRoomTypeAsync(command.RoomId))
                .ReturnsAsync(room);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Room or RoomType not found.");
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenCheckOutDateIsBeforeCheckIn()
        {
            var command = new CreateBookingCommand
            {
                RoomId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(-1)
            };

            var room = new Room
            {
                Id = command.RoomId,
                RoomType = new RoomType { PricePerNight = 50 }
            };

            _roomRepositoryMock.Setup(r => r.GetByIdWithRoomTypeAsync(command.RoomId))
                .ReturnsAsync(room);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<System.ComponentModel.DataAnnotations.ValidationException>()
                .WithMessage("Invalid check-in/check-out dates.");
        }
        [Fact]
        public async Task Handle_ShouldCalculateCorrectTotalPrice()
        {
            var command = new CreateBookingCommand
            {
                RoomId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(4) // 4 nights
            };

            var room = new Room
            {
                Id = command.RoomId,
                RoomType = new RoomType { PricePerNight = 200 }
            };

            Booking? capturedBooking = null;
            var expectedId = Guid.NewGuid();

            _roomRepositoryMock.Setup(r => r.GetByIdWithRoomTypeAsync(command.RoomId))
                .ReturnsAsync(room);

            _bookingRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<Booking>()))
                .Callback<Booking>(b =>
                {
                    b.Id = expectedId;
                    capturedBooking = b;
                })
                .ReturnsAsync(() => capturedBooking!);

            _bookingRepositoryMock.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(expectedId);
            capturedBooking!.Price.Should().Be(800); // 4 nights * 200
        }

    }
}
