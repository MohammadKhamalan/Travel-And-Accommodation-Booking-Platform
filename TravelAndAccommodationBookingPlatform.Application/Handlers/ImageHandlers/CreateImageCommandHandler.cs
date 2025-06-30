using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.ImageCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Guid>
{
    private readonly IImageRepository _repository;
    private readonly IMapper _mapper;

    public CreateImageCommandHandler(IImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var image = _mapper.Map<Image>(request);
        await _repository.InsertAsync(image);
        await _repository.SaveChangesAsync();
        return image.Id;
    }
}
