using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;
namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IDiscountRepository : IGenericRepository<Discount>
{
  
    public Task<Discount?> GetByIdAsync(Guid discountId);
    Task<PaginatedList<Discount>> GetAllByRoomTypeIdAsync(Guid roomTypeId, int pageNumber, int pageSize);
    Task<bool> HasOverlappingDiscountAsync(Guid roomTypeId, DateTime fromDate, DateTime toDate);
}

