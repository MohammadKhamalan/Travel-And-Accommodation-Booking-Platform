using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.UserQueries;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="command">The user registration data.</param>
    /// <returns>The ID of the created user.</returns>
    /// <response code="200">Returns the user ID.</response>
    /// <response code="400">If the model is invalid.</response>
    /// <response code="500">If an internal error occurs.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred during registration: {ex.Message}");
        }
    }


    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>List of all users</returns>
    /// <response code="200">Returns the list of users</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all users.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User information</returns>
    /// <response code="200">Returns the user</response>
    /// <response code="404">If user not found</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (user == null)
                return NotFound($"User with ID '{id}' not found.");

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving user with ID {UserId}.", id);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <response code="204">User deleted successfully</response>
    /// <response code="404">User not found</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"User with ID '{id}' not found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting user with ID {UserId}.", id);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
