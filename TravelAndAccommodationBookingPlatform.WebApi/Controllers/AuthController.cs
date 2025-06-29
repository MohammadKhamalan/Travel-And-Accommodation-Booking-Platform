using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

namespace TravelAndAccommodationBookingPlatform.API.Controllers;

[ApiController]
[Route("api/Authentication")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Login a user.
    /// </summary>
    /// <param name="query">The login data.</param>
    /// <returns>A JWT token if successful.</returns>
    /// <response code="200">Returns the JWT token.</response>
    /// <response code="400">If the model is invalid.</response>
    /// <response code="401">If credentials are invalid.</response>
    /// <response code="500">If an internal error occurs.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _mediator.Send(query);

            if (result == null)
                return Unauthorized("Invalid credentials.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during login: {ex.Message}");
        }
    }
}
