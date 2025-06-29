using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/discounts")]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiscountController> _logger;

    public DiscountController(IMediator mediator, ILogger<DiscountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all discounts for a specific room type with pagination.
    /// </summary>
    [HttpGet("roomtype/{roomTypeId}")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<DiscountDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAllByRoomType(Guid roomTypeId, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var query = new GetDiscountsByRoomTypeQuery
            {
                RoomTypeId = roomTypeId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PageData);
            return Ok(result.Items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting discounts for room type {RoomTypeId}", roomTypeId);
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Retrieves a discount by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(DiscountDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetDiscountByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting discount with ID {Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Checks if there's an overlapping discount for a given room type and date range.
    /// </summary>
    [HttpGet("overlap")]
    [Authorize]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CheckOverlap([FromQuery] Guid roomTypeId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        try
        {
            var query = new HasOverlappingDiscountQuery
            {
                RoomTypeId = roomTypeId,
                FromDate = fromDate,
                ToDate = toDate
            };

            var hasOverlap = await _mediator.Send(query);
            return Ok(hasOverlap);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking overlap for discount.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Creates a new discount.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating a new discount.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Deletes a discount by ID.
    /// </summary>
    /// <param name="id">The ID of the discount to delete</param>
    /// <returns>No content if deleted successfully</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteDiscountCommand { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Discount with ID {Id} not found for deletion.", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting discount with ID {Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }
}
