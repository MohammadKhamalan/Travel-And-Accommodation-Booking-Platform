using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HotelController> _logger;

    public HotelController(IMediator mediator, ILogger<HotelController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all hotels with optional search and pagination.
    /// </summary>
    /// <param name="searchQuery">Optional search query to filter hotels by name or address.</param>
    /// <param name="pageNumber">Page number for pagination (default is 1).</param>
    /// <param name="pageSize">Page size for pagination (default is 10).</param>
    /// <returns>List of hotels with pagination metadata in response headers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HotelDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllHotelsQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PageData);
        return Ok(result.Items);
    }

    /// <summary>
    /// Retrieves a hotel by its ID.
    /// </summary>
    /// <param name="id">Hotel ID.</param>
    /// <returns>The hotel details if found; otherwise, 404.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(HotelDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetHotelByIdQuery { HotelId = id });

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Retrieves featured hotel deals.
    /// </summary>
    /// <param name="count">Number of deals to retrieve (default is 5).</param>
    /// <returns>List of featured hotel deals.</returns>
    [HttpGet("featured")]
    [ProducesResponseType(typeof(IEnumerable<FeaturedHotelDto>), 200)]
    public async Task<IActionResult> GetFeaturedDeals([FromQuery] int count = 5)
    {
        var result = await _mediator.Send(new GetFeaturedDealsQuery { Count = count });
        return Ok(result);
    }

    /// <summary>
    /// Searches hotels based on filter parameters.
    /// </summary>
    /// <param name="parameters">Search filters including city, price range, rating, etc.</param>
    /// <returns>List of matching hotels with room data.</returns>
    [HttpPost("search")]
    [ProducesResponseType(typeof(PaginatedList<HotelSearchResultDto>), 200)]
    public async Task<IActionResult> Search([FromBody] HotelSearchParameters parameters)
    {
        var result = await _mediator.Send(new GetHotelSearchResultsQuery { Parameters = parameters });
        return Ok(result);
    }

    /// <summary>
    /// Retrieves available rooms for a hotel within the specified date range.
    /// </summary>
    /// <param name="hotelId">Hotel ID.</param>
    /// <param name="checkInDate">Check-in date.</param>
    /// <param name="checkOutDate">Check-out date.</param>
    /// <returns>List of available rooms.</returns>
    [HttpGet("{hotelId}/available-rooms")]
    [ProducesResponseType(typeof(IEnumerable<Room>), 200)]
    public async Task<IActionResult> GetAvailableRooms(Guid hotelId, [FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate)
    {
        var result = await _mediator.Send(new GetHotelAvailableRoomsQuery
        {
            HotelId = hotelId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate
        });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new hotel.
    /// </summary>
    /// <param name="dto">The hotel data to create.</param>
    /// <returns>The ID of the created hotel.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateHotelRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = new CreateHotelCommand
        {
            Name = dto.Name,
            Description = dto.Description,
            StreetAddress = dto.StreetAddress,
            PhoneNumber = dto.PhoneNumber,
            NumberOfRooms = dto.NumberOfRooms,
            Rating = dto.Rating,
            CityId = dto.CityId,
            OwnerId = dto.OwnerId
        };

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Updates an existing hotel.
    /// </summary>
    /// <param name="id">Hotel ID to update.</param>
    /// <param name="dto">Updated hotel details.</param>
    /// <returns>The updated hotel data.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(HotelDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHotelRequestDto dto)
    {
        var command = new UpdateHotelCommand
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            StreetAddress = dto.StreetAddress,
            PhoneNumber = dto.PhoneNumber,
            NumberOfRooms = dto.NumberOfRooms,
            Rating = dto.Rating,
            CityId = dto.CityId
        };

        var updated = await _mediator.Send(command);
        return Ok(updated);
    }

    /// <summary>
    /// Deletes a hotel by its ID.
    /// </summary>
    /// <param name="id">Hotel ID to delete.</param>
    /// <returns>No content if deleted successfully.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteHotelCommand { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
