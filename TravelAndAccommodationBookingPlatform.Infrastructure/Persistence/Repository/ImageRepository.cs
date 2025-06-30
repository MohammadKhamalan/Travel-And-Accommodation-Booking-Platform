using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ImageRepository> _logger;

    public ImageRepository(ApplicationDbContext context, ILogger<ImageRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Image?> GetByIdAsync(Guid id)
    {
        return await _context.Images.FindAsync(id);
    }

    public async Task<List<Image>> GetImagesByHotelIdAsync(Guid hotelId)
    {
        return await _context.Images
            .Where(img => img.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<List<Image>> GetImagesByCityIdAsync(Guid cityId)
    {
        return await _context.Images
            .Where(img => img.CityId == cityId)
            .ToListAsync();
    }

    public async Task<Image> InsertAsync(Image image)
    {
        try
        {
            await _context.Images.AddAsync(image);
            return image;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to insert image due to constraint violation.");
            throw new DataConstraintViolationException("Image insert failed due to constraint violation.");
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var image = await _context.Images.FindAsync(id);
        if (image == null)
            throw new NotFoundException($"Image with ID {id} not found.");

        _context.Images.Remove(image);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
