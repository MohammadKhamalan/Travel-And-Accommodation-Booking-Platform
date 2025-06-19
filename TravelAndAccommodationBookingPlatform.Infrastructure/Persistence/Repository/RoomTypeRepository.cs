using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoomTypeRepository> _logger;
        public RoomTypeRepository(ApplicationDbContext context, ILogger<RoomTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<RoomType> InsertAsync(RoomType entity)
        {
            try
            {
                await _context.RoomTypes.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error inserting room type.");
                throw new DataConstraintViolationException("Room type insert failed due to constraint violation.");
            }
        }

        public async Task UpdateAsync(RoomType entity)
        {
            if (!await IsExistsAsync(entity.RoomTypeId))
                throw new NotFoundException($"RoomType with ID '{entity.RoomTypeId}' not found.");

            _context.RoomTypes.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
                throw new NotFoundException($"RoomType with ID '{id}' not found.");

            _context.RoomTypes.Remove(roomType);
            await SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RoomType?> GetByIdAsync(Guid roomTypeId)
        {
            try
            {
                return await _context.RoomTypes
                    .Include(rt => rt.Amenities)
                    .Include(rt => rt.Discounts)
                    .FirstOrDefaultAsync(rt => rt.RoomTypeId == roomTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching room type with ID {roomTypeId}");
                return null;
            }
        }

        public async Task<PaginatedList<RoomType>> GetAllAsync(Guid hotelId, bool includeAmenities, int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.RoomTypes
                    .Where(rt => rt.Rooms.Any(r => r.HotelId == hotelId))
                    .AsQueryable();

                if (includeAmenities)
                    query = query.Include(rt => rt.Amenities);

                var totalCount = await query.CountAsync();

                var roomTypes = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<RoomType>(roomTypes, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching room types for hotel ID {hotelId}");
                return new PaginatedList<RoomType>(new List<RoomType>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<bool> CheckRoomTypeExistenceForHotel(Guid hotelId, Guid roomTypeId)
        {
            try
            {
                return await _context.Rooms
                    .AnyAsync(r => r.HotelId == hotelId && r.RoomTypeId == roomTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking room type existence for hotel.");
                return false;
            }
        }
    }
}
