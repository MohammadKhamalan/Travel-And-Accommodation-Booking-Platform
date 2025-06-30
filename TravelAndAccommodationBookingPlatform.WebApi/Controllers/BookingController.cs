using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net.Mail;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Interfaces;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.WebApi.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBookingRepository _bookingRepository;
    private readonly IInvoiceService _invoiceService;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<BookingController> _logger;

    public BookingController(
        IMediator mediator,
        IBookingRepository bookingRepository,
        IInvoiceService invoiceService,
        IPdfGenerator pdfGenerator,
        IEmailSender emailSender,
        ILogger<BookingController> logger)
    {
        _mediator = mediator;
        _bookingRepository = bookingRepository;
        _invoiceService = invoiceService;
        _pdfGenerator = pdfGenerator;
        _emailSender = emailSender;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new booking, generates an invoice PDF, and sends it via email to the guest.
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
    {
        try
        {
            
            var isAvailable = await _mediator.Send(new CanBookRoomQuery
            {
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate
            });

            if (!isAvailable)
            {
                return BadRequest("This room is already booked during the selected date range.");
            }

          
            var command = new CreateBookingCommand
            {
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate
            };

            var bookingId = await _mediator.Send(command);

     
            var invoiceEntity = await _bookingRepository.GetInvoiceByBookingIdAsync(bookingId);
            if (invoiceEntity == null)
                return StatusCode(500, "Failed to fetch invoice details.");

            string guestName = invoiceEntity.GuestName;
            string guestEmail = invoiceEntity.GuestEmail;

            var html = _invoiceService.GenerateInvoiceHtml(invoiceEntity, guestName);
            var pdf = await _pdfGenerator.GeneratePdfFromHtml(html);
            var attachment = new Attachment(new MemoryStream(pdf), "invoice.pdf", MediaTypeNames.Application.Pdf);

            await _emailSender.SendEmailAsync(guestEmail, "Your Booking Invoice", "Thank you for booking!", attachment);

            return CreatedAtAction(nameof(GetById), new { bookingId }, bookingId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Booking creation or invoice email failed.");
            return StatusCode(500, "An error occurred during booking.");
        }
    }

    /// <summary>
    /// Retrieves a booking by ID.
    /// </summary>
    [HttpGet("{bookingId}")]
    [ProducesResponseType(typeof(BookingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid bookingId)
    {
        var result = await _mediator.Send(new GetBookingByIdQuery { BookingId = bookingId });
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retrieves paginated bookings for a hotel.
    /// </summary>
    [HttpGet("by-hotel/{hotelId}")]
    [Authorize]
    [ProducesResponseType(typeof(PaginatedList<BookingResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByHotel(Guid hotelId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetBookingsByHotelIdQuery
        {
            HotelId = hotelId,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.PageData));
        return Ok(result);
    }

    /// <summary>
    /// Deletes a booking.
    /// <param name="bookingId">Booking ID.</param>
    /// </summary>
    [HttpDelete("{bookingId}")]
    [Authorize(Roles = "Admin,Owner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid bookingId)
    {
        await _mediator.Send(new DeleteBookingCommand { BookingId = bookingId });
        return Ok();
    }

    /// <summary>
    /// Checks if a booking exists for a guest.
    /// </summary>
    [HttpGet("check-existence")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckExistence([FromQuery] Guid bookingId, [FromQuery] string guestEmail)
    {
        var result = await _mediator.Send(new CheckBookingExistenceForGuestQuery
        {
            BookingId = bookingId,
            GuestEmail = guestEmail
        });

        return Ok(result);
    }

    /// <summary>
    /// Checks if a room is available for booking within a date range.
    /// </summary>
    [HttpGet("can-book")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CanBook([FromQuery] Guid roomId, [FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
    {
        var result = await _mediator.Send(new CanBookRoomQuery
        {
            RoomId = roomId,
            CheckInDate = checkIn,
            CheckOutDate = checkOut
        });

        return Ok(result);
    }

    /// <summary>
    /// Generates and returns the PDF invoice for the specified booking.
    /// <param name="bookingId">Booking ID.</param>
    /// </summary>
    [HttpGet("{bookingId}/invoice")]
    [Authorize]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInvoice(Guid bookingId)
    {
        try
        {
            
            var invoice = await _bookingRepository.GetInvoiceByBookingIdAsync(bookingId);
            if (invoice == null)
                return NotFound($"No invoice found for booking ID {bookingId}");

           
            string html = _invoiceService.GenerateInvoiceHtml(invoice, invoice.GuestName);
            byte[] pdfBytes = await _pdfGenerator.GeneratePdfFromHtml(html);

            
            return File(pdfBytes, "application/pdf", $"invoice_{bookingId}.pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to generate invoice for booking ID: {bookingId}");
            return StatusCode(500, "An error occurred while generating the invoice PDF.");
        }
    }

}
