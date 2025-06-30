using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomTypeCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomTypeDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomTypeQueries;

using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/room-types")]
[Authorize(Roles = "Admin")] 
public class RoomTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoomTypesController> _logger;

    public RoomTypesController(IMediator mediator, ILogger<RoomTypesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves a paginated list of room types for a specific hotel.
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <param name="includeAmenities">Include amenities</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    [HttpGet("{hotelId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoomTypesByHotel(Guid hotelId, [FromQuery] bool includeAmenities = false, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetRoomTypesByHotelQuery
            {
                HotelId = hotelId,
                IncludeAmenities = includeAmenities,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PageData);

            return Ok(result.Items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch room types for hotel ID: {HotelId}", hotelId);
            return StatusCode(500, "An error occurred while fetching room types.");
        }
    }

    /// <summary>
    /// Creates a new room type.
    /// </summary>
    /// <param name="command">Room type creation command</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateRoomType([FromBody] CreateRoomTypeCommand command)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRoomType), new { id }, id);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create a new room type.");
            return StatusCode(500, "An error occurred while creating room type.");
        }
    }
}
