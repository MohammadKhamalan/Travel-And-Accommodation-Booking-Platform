using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface ICityRepository : IGenericRepository<City>
{
    
    Task<PaginatedList<City>> GetAllAsync(bool includeHotels, string? searchQuery, int pageNumber, int pageSize);
    Task<City?> GetByIdAsync(Guid cityId, bool includeHotels);
    Task<List<City>> GetTrendingCitiesAsync(int count);
}

