using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;


public interface IReviewRepository : IGenericRepository<Review>
{
    public Task<PaginatedList<Review>> GetAllByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNumber, int pageSize);
    public Task<Review?> GetByIdAsync(Guid reviewId);
   
    Task<bool> DoesBookingHaveReviewAsync(Guid bookingId);
}
