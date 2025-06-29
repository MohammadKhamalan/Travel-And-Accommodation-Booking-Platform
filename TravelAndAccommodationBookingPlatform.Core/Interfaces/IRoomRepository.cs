using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;
public interface IRoomRepository : IGenericRepository<Room>
{

    Task<PaginatedList<Room>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<Room?> GetByIdAsync(Guid roomId);
    Task<PaginatedList<Room>> GetRoomsByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNumber, int pageSize);
    Task<bool> CheckRoomBelongsToHotelAsync(Guid hotelId, Guid roomId);
    Task<float> GetPriceForRoomWithDiscount(Guid roomId);
    Task<Room?> GetByIdWithRoomTypeAsync(Guid roomId);

}
