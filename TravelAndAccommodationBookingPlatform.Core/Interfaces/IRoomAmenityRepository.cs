using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IRoomAmenityRepository
{
    public Task<PaginatedList<Amenity>>
    GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<Amenity?> GetByIdAsync(Guid amenityId);
    public Task<Amenity?> InsertAsync(Amenity roomAmenity);
    public Task UpdateAsync(Amenity roomAmenity);
    public Task DeleteAsync(Guid amenityId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistsAsync(Guid amenityId);
}
