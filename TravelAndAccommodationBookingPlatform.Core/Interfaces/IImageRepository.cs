using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IImageRepository
{
    Task<Image?> GetByIdAsync(Guid id);
    Task<List<Image>> GetImagesByHotelIdAsync(Guid hotelId);
    Task<List<Image>> GetImagesByCityIdAsync(Guid cityId);
    Task<Image> InsertAsync(Image image);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}
