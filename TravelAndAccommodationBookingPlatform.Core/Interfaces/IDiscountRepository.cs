using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;
namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IDiscountRepository
{
    public Task<PaginatedList<Discount>>
      GetAllByRoomTypeIdAsync(Guid roomTypeId, int pageNumber, int pageSize);
    public Task<Discount?> GetByIdAsync(Guid discountId);
    public Task<bool> HasOverlappingDiscountAsync(Guid roomTypeId,
        DateTime fromDate,
        DateTime toDate);
    public Task<Discount?> InsertAsync(Discount discount);
    public Task DeleteAsync(Guid discountId);
    public Task SaveChangesAsync();
    public Task<bool> IsExistsAsync(Guid discountId);
}
