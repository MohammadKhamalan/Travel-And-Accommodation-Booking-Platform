using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;


public interface IHotelRepository : IGenericRepository<Hotel>
{
  
    public Task<Hotel?> GetByIdAsync(Guid hotelId);
    Task<PaginatedList<Hotel>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
    Task<List<Room>> GetHotelAvailableRoomsAsync(Guid hotelId, DateTime checkInDate, DateTime checkOutDate);
    Task<PaginatedList<HotelSearchResult>> HotelSearchAsync(HotelSearchParameters searchParams);
    Task<List<FeaturedDeal>> GetFeaturedDealsAsync(int count);
    Task<bool> IsHotelNameAndAddressDuplicateAsync(string name, string address);

}
