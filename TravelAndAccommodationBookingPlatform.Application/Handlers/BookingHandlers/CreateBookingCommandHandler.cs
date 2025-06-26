using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = new Booking
        {
            RoomId = request.RoomId,
            UserId = request.UserId,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            BookingDate = DateTime.UtcNow
        };

        await _repository.InsertAsync(booking);
        await _repository.SaveChangesAsync();

        return booking.Id;
    }
}
