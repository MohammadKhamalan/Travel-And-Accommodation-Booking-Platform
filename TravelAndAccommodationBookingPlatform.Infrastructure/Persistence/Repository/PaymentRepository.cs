using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(ApplicationDbContext context, ILogger<PaymentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Payment>> GetAllAsync()
        {
            try
            {
                return await _context.Payments
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all payments.");
                return Array.Empty<Payment>();
            }
        }

        public async Task<Payment?> GetByIdAsync(Guid paymentId)
        {
            try
            {
                return await _context.Payments.FindAsync(paymentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving payment with ID: {paymentId}");
                return null;
            }
        }

        public async Task<Payment> InsertAsync(Payment entity)
        {
            try
            {
                await _context.Payments.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to insert payment.");
                throw new DataConstraintViolationException("Error inserting payment. Ensure required fields are valid.");
            }
        }

        public async Task UpdateAsync(Payment entity)
        {
            var exists = await IsExistsAsync(entity.Id);
            if (!exists)
                throw new NotFoundException($"Payment with ID '{entity.Id}' not found.");

            _context.Payments.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException($"Payment with ID '{id}' not found.");

            _context.Payments.Remove(payment);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Payments.AnyAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
