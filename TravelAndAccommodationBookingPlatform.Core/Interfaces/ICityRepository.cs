using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface ICityRepository
{
    public Task<PaginatedList<City>>
    GetAllAsync(bool includeHotels, string? searchQuery, int pageNumber, int pageSize);
    public Task<City?> GetByIdAsync(Guid cityId, bool includeHotels);
    public Task<City?> InsertAsync(City city);
    public Task UpdateAsync(City city);
    public Task DeleteAsync(Guid cityId);
    public Task<List<City>> GetTrendingCitiesAsync(int count);
    public Task SaveChangesAsync();
    public Task<bool> IsExistsAsync(Guid cityId);
}
