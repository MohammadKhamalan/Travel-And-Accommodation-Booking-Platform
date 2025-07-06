using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoomController> _logger;

    public RoomController(IMediator mediator, ILogger<RoomController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all rooms with optional search and pagination.
    /// </summary>
    /// <param name="searchQuery">Optional search keyword.</param>
    /// <param name="pageNumber">Page number (default is 1).</param>
    /// <param name="pageSize">Page size (default is 10).</param>
    /// <returns>Paginated list of rooms.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<RoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] string? searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllRoomsQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.PageData));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves room details by room ID.
    /// </summary>
    /// <param name="roomId">Room ID.</param>
    [HttpGet("{roomId}")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid roomId)
    {
        var room = await _mediator.Send(new GetRoomByIdQuery { RoomId = roomId });
        return room is null ? NotFound() : Ok(room);
    }

    /// <summary>
    /// Retrieves rooms for a specific hotel.
    /// </summary>
    /// <param name="hotelId">Hotel ID.</param>
    /// <param name="searchQuery">Search query (optional).</param>
    /// <param name="pageNumber">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    [HttpGet("hotel/{hotelId}")]
    [ProducesResponseType(typeof(PaginatedList<RoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoomsByHotel(Guid hotelId, string? searchQuery = null, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetRoomsByHotelIdQuery
        {
            HotelId = hotelId,
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.PageData));
        return Ok(result);
    }

    /// <summary>
    /// Creates a new room.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
    {
        var command = new CreateRoomCommand
        {
            HotelId = dto.HotelId,
            RoomTypeId = dto.RoomTypeId,
            AdultsCapacity = dto.AdultsCapacity,
            ChildrenCapacity = dto.ChildrenCapacity,
            Rating = dto.Rating
        };

        var roomId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { roomId }, roomId);
    }

    /// <summary>
    /// Updates an existing room.
    /// </summary>
    /// <param name="roomId">The ID of the room to update.</param>
    /// <param name="dto">The updated room data.</param>
    /// <returns>The updated room data.</returns>
    [HttpPut("{roomId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid roomId, [FromBody] UpdateRoomDto dto)
    {
        if (roomId != dto.Id)
            return BadRequest("URL roomId does not match body Id.");

        var command = new UpdateRoomCommand
        {
            Id = roomId,
            AdultsCapacity = dto.AdultsCapacity,
            ChildrenCapacity = dto.ChildrenCapacity,
            Rating = dto.Rating,
            ModifiedAt = dto.ModifiedAt
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }


    /// <summary>
    /// Deletes a room by ID.
    /// /// <param name="roomId">The ID of the room to delete.</param>
    /// </summary>
    [HttpDelete("{roomId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(Guid roomId)
    {
        await _mediator.Send(new DeleteRoomCommand { RoomId = roomId });
        return Ok();
    }

    /// <summary>
    /// Gets the final price for a room after applying any discounts.
    /// </summary>
    [HttpGet("{roomId}/final-price")]
    [ProducesResponseType(typeof(float), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDiscountedPrice(Guid roomId)
    {
        var price = await _mediator.Send(new GetPriceForRoomWithDiscountQuery { RoomId = roomId });
        return Ok(price);
    }
}
