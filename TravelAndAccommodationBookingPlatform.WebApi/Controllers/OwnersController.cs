using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/owners")]
[Authorize(Roles = "Admin")] 
public class OwnersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OwnersController> _logger;

    public OwnersController(IMediator mediator, ILogger<OwnersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all owners.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<OwnerDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllOwnersQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all owners.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Get an owner by ID.
    /// <param name="id">The unique identifier of the owner.</param>
    /// <returns>The owner details.</returns>
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OwnerDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetOwnerByIdQuery { Id = id });
            if (result == null)
                return NotFound($"Owner with ID '{id}' not found.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving owner with ID {Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Create a new owner.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new CreateOwnerCommand
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber
            };

            var ownerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = ownerId }, ownerId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new owner.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Update an existing owner.
    /// <param name="id">The unique identifier of the owner.</param>
    /// <returns>The owner details.</returns>
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OwnerDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
    {
        if (!ModelState.IsValid || id != dto.Id)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateOwnerCommand
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber
            };

            var updated = await _mediator.Send(command);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating owner with ID {Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Delete an owner by ID.
    /// <param name="id">The unique identifier of the owner.</param>
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteOwnerCommand { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Owner with ID {Id} not found for deletion.", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting owner with ID {Id}", id);
            return StatusCode(500, "Internal server error.");
        }
    }
}
