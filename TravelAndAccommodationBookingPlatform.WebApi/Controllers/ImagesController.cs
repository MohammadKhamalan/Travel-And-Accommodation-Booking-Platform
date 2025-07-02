using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.ImageCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ImageDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.ImageHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.ImageQueries;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/images")]

public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IMediator mediator, ILogger<ImagesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new image.
    /// <returns>The id of image created.</returns>
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateImageCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating image.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Retrieves images by hotel ID.
    /// </summary>
    [HttpGet("hotel/{hotelId}")]
    [ProducesResponseType(typeof(IEnumerable<ImageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByHotel(Guid hotelId)
    {
        var result = await _mediator.Send(new GetImagesByHotelIdQuery(hotelId));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves images by city ID.
    /// </summary>
    [HttpGet("city/{cityId}")]
    [ProducesResponseType(typeof(IEnumerable<ImageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCity(Guid cityId)
    {
        var result = await _mediator.Send(new GetImagesByCityIdQuery(cityId));
        return Ok(result);
    }

    /// <summary>
    /// Gets image by ID.
    /// <param name="id">The unique identifier of the image.</param>
    /// <returns>The image details.</returns>
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ImageDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetImageByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Deletes an image by ID.
    /// <param name="id">The unique identifier of the image.</param>
    /// <returns>The review details.</returns>
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteImageCommand(id));
        return NoContent();
    }
}
