using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.ImageCommands;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ImageHandlers
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly IImageRepository _repository;

        public DeleteImageCommandHandler(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _repository.GetByIdAsync(request.Id);
            if (image == null)
                throw new NotFoundException($"Image with ID {request.Id} not found.");

            await _repository.DeleteAsync(request.Id);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
