using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Guid>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;

    public CreateDiscountCommandHandler(IDiscountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = new Discount
        {
            Id = Guid.NewGuid(),
            RoomTypeId = request.RoomTypeId,
            DiscountPercentage = request.DiscountPercentage,
            FromDate = request.FromDate,
            ToDate = request.ToDate
        };

        await _repository.InsertAsync(discount);
        await _repository.SaveChangesAsync();

        return discount.Id;
    }

}
