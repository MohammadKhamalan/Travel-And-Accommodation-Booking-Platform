using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    public Task<IReadOnlyList<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(Guid paymentId);
}
