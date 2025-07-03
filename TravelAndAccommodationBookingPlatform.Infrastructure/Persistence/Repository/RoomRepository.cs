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

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RoomRepository> _logger;
    public RoomRepository(ApplicationDbContext context, ILogger<RoomRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Room> InsertAsync(Room entity)
    {
        try
        {
            await _context.Rooms.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error inserting room.");
            throw new DataConstraintViolationException("Room insert failed due to constraint violation.");
        }
    }

    public async Task UpdateAsync(Room entity)
    {
        if (!await IsExistsAsync(entity.Id))
            throw new NotFoundException($"Room with ID '{entity.Id}' not found.");

        _context.Rooms.Update(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            throw new NotFoundException($"Room with ID '{id}' not found.");

        _context.Rooms.Remove(room);
        await SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(Guid id)
    {
        return await _context.Rooms.AnyAsync(r => r.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Room?> GetByIdAsync(Guid roomId)
    {
        try
        {
            return await _context.Rooms
        .Include(r => r.Hotel)
            .ThenInclude(h => h.Owner)
        .Include(r => r.RoomType)
        .FirstOrDefaultAsync(r => r.Id == roomId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching room with ID {roomId}");
            return null;
        }
    }

    public async Task<PaginatedList<Room>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        var query = _context.Rooms
            .Include(r => r.RoomType)
            .Include(r => r.Hotel)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(r =>
                r.RoomType.RoomCategory.ToString().Contains(searchQuery) ||
                r.Hotel.Name.Contains(searchQuery));
        }

        var totalCount = await query.CountAsync();
        var rooms = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Room>(rooms, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedList<Room>> GetRoomsByHotelIdAsync(Guid hotelId, string? searchQuery, int pageNumber, int pageSize)
    {
        var query = _context.Rooms
            .Include(r => r.RoomType)
            .Include(r => r.Hotel)
            .Where(r => r.HotelId == hotelId);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(r => r.RoomType.RoomCategory.ToString().Contains(searchQuery));
        }

        var totalCount = await query.CountAsync();
        var rooms = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Room>(rooms, totalCount, pageNumber, pageSize);
    }

    public async Task<bool> CheckRoomBelongsToHotelAsync(Guid hotelId, Guid roomId)
    {
        return await _context.Rooms.AnyAsync(r => r.HotelId == hotelId && r.Id == roomId);
    }

    public async Task<float> GetPriceForRoomWithDiscount(Guid roomId)
    {
        var room = await _context.Rooms
            .Include(r => r.RoomType)
                .ThenInclude(rt => rt.Discounts)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null)
            throw new NotFoundException($"Room with ID '{roomId}' not found.");

        var today = DateTime.UtcNow;
        var discount = room.RoomType.Discounts
            .Where(d => today >= d.FromDate && today <= d.ToDate)
            .OrderByDescending(d => d.DiscountPercentage)
            .FirstOrDefault();

        decimal price = room.RoomType.PricePerNight;
        if (discount != null)
        {
            var discountAmount = price * (decimal)(discount.DiscountPercentage / 100);
            price -= discountAmount;
        }

        return (float)price;
    }
    public async Task<Room?> GetByIdWithRoomTypeAsync(Guid roomId)
    {
        return await _context.Rooms
            .Include(r => r.RoomType)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }

}
