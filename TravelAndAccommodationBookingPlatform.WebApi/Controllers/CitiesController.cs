using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CitiesController> _logger;

    public CitiesController(IMediator mediator, ILogger<CitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves a paginated list of all cities.
    /// </summary>
    /// <param name="includeHotels">Whether to include hotel details.</param>
    /// <param name="searchQuery">Search keyword (optional).</param>
    /// <param name="pageNumber">Page number (default is 1).</param>
    /// <param name="pageSize">Number of items per page (default is 10).</param>
    /// <returns>Paginated list of cities.</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeHotels = false, [FromQuery] string? searchQuery = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllCitiesQuery
        {
            IncludeHotels = includeHotels,
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.PageData));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a list of trending cities.
    /// </summary>
    /// <param name="count">Number of trending cities to retrieve.</param>
    /// <returns>List of trending cities.</returns>
    [HttpGet("trending")]
    [Authorize]
    [ProducesResponseType(typeof(List<TrendingCityResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTrending([FromQuery] int count = 5)
    {
        var result = await _mediator.Send(new GetTrendingCitiesQuery { Count = count });
        return Ok(result);
    }

    /// <summary>
    /// Creates a new city.
    /// </summary>
    /// <param name="dto">City details.</param>
    /// <returns>Newly created city ID.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCityDto dto)
    {
        var command = new CreateCityCommand
        {
            Name = dto.Name,
            CountryName = dto.CountryName,
            PostOffice = dto.PostOffice
        };

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Updates an existing city.
    /// </summary>
    /// <param name="id">City ID.</param>
    /// <param name="dto">Updated city data.</param>
    /// <returns>Updated city.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCityDto dto)
    {
        var command = new UpdateCityCommand
        {
            Id = id,
            Name = dto.Name,
            CountryName = dto.CountryName,
            PostOffice = dto.PostOffice
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a city by ID.
    /// </summary>
    /// <param name="id">City ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCityCommand { Id = id });
        return NoContent();
    }

    /// <summary>
    /// Retrieves a city by ID.
    /// </summary>
    /// <param name="id">City ID.</param>
    /// <param name="includeHotels">Include hotels data.</param>
    /// <returns>City details.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> GetById(Guid id, [FromQuery] bool includeHotels = false)
    {
        var city = await _mediator.Send(new GetCityByIdQuery { Id = id, IncludeHotels = includeHotels });

        if (city == null)
            return NotFound();

        return Ok(city);
    }
}
