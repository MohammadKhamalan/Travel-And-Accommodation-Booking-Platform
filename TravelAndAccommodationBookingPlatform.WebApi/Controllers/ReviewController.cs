using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IMediator mediator, ILogger<ReviewController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new review for a completed booking.
    /// </summary>
    /// <param name="dto">The review details including booking ID, rating, and optional comment.</param>
    /// <returns>The ID of the created review.</returns>
    /// <response code="201">Review created successfully.</response>
    /// <response code="400">Validation failed or booking does not exist.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
    {
        var command = new CreateReviewCommand
        {
            BookingId = dto.BookingId,
            Comment = dto.Comment,
            Rating = dto.Rating
        };

        var reviewId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { reviewId }, reviewId);
    }

    /// <summary>
    /// Updates an existing review by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the review to update.</param>
    /// <param name="dto">The updated comment and rating.</param>
    /// <returns>The updated review details.</returns>
    /// <response code="200">Review updated successfully.</response>
    /// <response code="404">Review not found with the given ID.</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewCommand dto)
    {
        dto.Id = id;
        var updated = await _mediator.Send(dto);
        return Ok(updated);
    }

    /// <summary>
    /// Retrieves paginated reviews for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <param name="searchQuery">Optional keyword to search in comments.</param>
    /// <param name="pageNumber">The page number for pagination (default is 1).</param>
    /// <param name="pageSize">Number of reviews per page (default is 10).</param>
    /// <returns>Paginated list of hotel reviews.</returns>
    /// <response code="200">List of reviews successfully retrieved.</response>
    [HttpGet("hotel/{hotelId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedList<ReviewDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByHotelId(Guid hotelId, string? searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetReviewsByHotelQuery
        {
            HotelId = hotelId,
            SearchQuery = searchQuery,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var paged = await _mediator.Send(query);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paged.PageData));
        return Ok(paged);
    }

    /// <summary>
    /// Retrieves a specific review by its ID.
    /// </summary>
    /// <param name="reviewId">The unique identifier of the review.</param>
    /// <returns>The review details.</returns>
    /// <response code="200">Review retrieved successfully.</response>
    /// <response code="404">No review found with the provided ID.</response>
    [HttpGet("{reviewId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid reviewId)
    {
        var result = await _mediator.Send(new GetReviewByIdQuery { ReviewId = reviewId });
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Checks if a review exists for a given booking.
    /// </summary>
    /// <param name="bookingId">The unique identifier of the booking.</param>
    /// <returns>True if a review exists; otherwise, false.</returns>
    /// <response code="200">Check completed successfully.</response>
    [HttpGet("booking/{bookingId}/exists")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> DoesBookingHaveReview(Guid bookingId)
    {
        var exists = await _mediator.Send(new DoesBookingHaveReviewQuery { BookingId = bookingId });
        return Ok(exists);
    }
}
