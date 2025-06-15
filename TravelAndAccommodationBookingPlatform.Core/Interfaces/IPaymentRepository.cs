using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces;

public interface IPaymentRepository
{
    public Task<IReadOnlyList<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(Guid paymentId);
    Task<Payment?> InsertAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Guid paymentId);
    Task SaveChangesAsync();
    Task<bool> IsExistsAsync(Guid paymentId);
}
