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
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OwnerRepository> _logger;

        public OwnerRepository(ApplicationDbContext context, ILogger<OwnerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Owner>> GetAllAsync()
        {
            try
            {
                return await _context.Owners
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all owners.");
                return Array.Empty<Owner>();
            }
        }

        public async Task<Owner?> GetByIdAsync(Guid ownerId)
        {
            try
            {
                return await _context.Owners.FindAsync(ownerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve owner with ID: {OwnerId}", ownerId);
                return null;
            }
        }

        public async Task<Owner> InsertAsync(Owner entity)
        {
            try
            {
                await _context.Owners.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting owner.");
                throw new DataConstraintViolationException("Error inserting owner. Check required fields or constraint violations.");
            }
        }

        public async Task UpdateAsync(Owner entity)
        {
            var exists = await IsExistsAsync(entity.Id);
            if (!exists)
                throw new NotFoundException($"Owner with ID '{entity.Id}' not found.");

            _context.Owners.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var owner = await GetByIdAsync(id);
            if (owner == null)
                throw new NotFoundException($"Owner with ID '{id}' not found.");

            _context.Owners.Remove(owner);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Owners.AnyAsync(o => o.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
