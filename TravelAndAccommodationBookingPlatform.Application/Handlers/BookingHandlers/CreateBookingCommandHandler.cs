using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;

    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Load room including RoomType
        var room = await _roomRepository.GetByIdWithRoomTypeAsync(request.RoomId);
        if (room == null || room.RoomType == null)
            throw new NotFoundException("Room or RoomType not found.");

        // Calculate number of nights
        var numberOfNights = (request.CheckOutDate - request.CheckInDate).Days;
        if (numberOfNights <= 0)
            throw new ValidationException("Invalid check-in/check-out dates.");

        // Calculate total price
        var price = numberOfNights * room.RoomType.PricePerNight;

        // Create and save booking
        var booking = new Booking
        {
            RoomId = request.RoomId,
            UserId = request.UserId,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            BookingDate = DateTime.UtcNow,
            Price = (double)price
        };

        await _bookingRepository.InsertAsync(booking);
        await _bookingRepository.SaveChangesAsync();

        return booking.Id;
    }
}
