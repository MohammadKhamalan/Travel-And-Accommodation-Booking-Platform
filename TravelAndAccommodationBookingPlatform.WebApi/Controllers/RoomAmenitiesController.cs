using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomAmenityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomAmenityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomAmenityQueries;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/amenities")]
public class RoomAmenitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoomAmenitiesController> _logger;

    public RoomAmenitiesController(IMediator mediator, ILogger<RoomAmenitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all room amenities with pagination and optional search.
    /// </summary>
    /// <param name="searchQuery">Optional search keyword.</param>
    /// <param name="pageNumber">Page number (default is 1).</param>
    /// <param name="pageSize">Page size (default is 10).</param>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PaginatedList<RoomAmenityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(string? searchQuery = null, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllRoomAmenitiesQuery
        {
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.PageData));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific amenity by its ID.
    /// </summary>
    /// <param name="id">Amenity ID.</param>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(RoomAmenityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRoomAmenityByIdQuery { AmenityId = id });
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new room amenity.
    /// </summary>
    /// <param name="dto">Details of the amenity to be created.</param>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateAmenityDto dto)
    {
        var command = new CreateRoomAmenityCommand
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Updates an existing room amenity.
    /// </summary>
    /// <param name="id">Amenity ID.</param>
    /// <param name="dto">Updated amenity details.</param>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RoomAmenityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAmenityDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID in URL must match ID in request body.");

        var command = new UpdateRoomAmenityCommand
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a room amenity by ID.
    /// </summary>
    /// <param name="id">Amenity ID.</param>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteRoomAmenityCommand { AmenityId = id });
        return Ok();
    }
}
