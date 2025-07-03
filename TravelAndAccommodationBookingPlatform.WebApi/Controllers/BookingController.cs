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
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using TravelAndAccommodationBookingPlatform.Application.Helpers;
using TravelAndAccommodationBookingPlatform.Core.Enums;

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
    private readonly IRoomRepository _roomRepository;
    private readonly IPendingBookingRepository _pendingBookingRepository;
    private readonly EmailTemplate _emailTemplate;

    public BookingController(
        IMediator mediator,
        IBookingRepository bookingRepository,
        IInvoiceService invoiceService,
        IPdfGenerator pdfGenerator,
        IEmailSender emailSender,
        ILogger<BookingController> logger,
        IRoomRepository roomRepository,
    IPendingBookingRepository pendingBookingRepository,
    EmailTemplate emailTemplate)
    {
        _mediator = mediator;
        _bookingRepository = bookingRepository;
        _invoiceService = invoiceService;
        _pdfGenerator = pdfGenerator;
        _emailSender = emailSender;
        _logger = logger;
        _roomRepository = roomRepository;
        _pendingBookingRepository = pendingBookingRepository;
        _emailTemplate = emailTemplate;
    }
    /// <summary>
    /// Creates a new pending booking, generates a draft invoice PDF, and sends it via email to the guest.
    /// </summary>
    [HttpPost("pending")]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePendingBooking([FromBody] PendingBookingCreateDto dto)
    {
        try
        {
            var isAvailable = await _mediator.Send(new CanBookRoomQuery(dto.RoomId, dto.CheckInDate, dto.CheckOutDate));
            if (!isAvailable)
            {
                return BadRequest("This room is already booked during the selected date range.");
            }

            var room = await _roomRepository.GetByIdAsync(dto.RoomId);
            if (room == null)
            {
                return BadRequest("Room not found.");
            }
            
            var invoiceEntity = await _pendingBookingRepository.GetByUserIdAsync(dto.UserId);
            if (invoiceEntity == null)
                return StatusCode(500, "Failed to fetch invoice details.");

            string guestName = invoiceEntity.FirstName;
            string guestEmail = invoiceEntity.Email;

           

           
            var pendingBookingId = Guid.NewGuid();

            var pendingBooking = new PendingBooking
            {
                Id = pendingBookingId,
                RoomId = dto.RoomId,
                UserId = dto.UserId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                CreatedAt = DateTime.UtcNow,
                Price = (double)room.RoomType.PricePerNight
            };

            await _pendingBookingRepository.InsertAsync(pendingBooking);

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow,
                GuestName = guestName,
                GuestEmail = guestEmail,
                HotelName = room.Hotel.Name,
                OwnerName = $"{room.Hotel.Owner.FirstName} {room.Hotel.Owner.LastName}",
                Price = (double)room.RoomType.PricePerNight
            };

            var html = _invoiceService.GenerateInvoiceHtml(invoice, guestName);
            var pdf = await _pdfGenerator.GeneratePdfFromHtml(html);
            var attachment = new Attachment(new MemoryStream(pdf), "draft-invoice.pdf", MediaTypeNames.Application.Pdf);

            await _emailSender.SendEmailAsync(guestEmail, "Your Booking Draft", "This is your pending booking invoice.", attachment);

            return CreatedAtAction(nameof(GetById), new { bookingId = pendingBooking.Id }, pendingBooking.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pending booking creation or draft invoice email failed.");
            return StatusCode(500, "An error occurred while creating the pending booking.");
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
        var result = await _mediator.Send(new GetBookingByIdQuery(bookingId));

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
        var invoice = await _bookingRepository.GetInvoiceByBookingIdAsync(bookingId);
        if (invoice == null)
            return NotFound("Booking not found or already deleted.");

        await _mediator.Send(new DeleteBookingCommand { BookingId = bookingId });

        var (subject, body) = _emailTemplate.GetEmailContent(PaymentStatus.Refunded, invoice.GuestName, invoice.HotelName);
        await _emailSender.SendEmailAsync(invoice.GuestEmail, subject, body);

        return Ok("Booking deleted and refund notification sent.");
    }


    /// <summary>
    /// Checks if a booking exists for a guest.
    /// </summary>
    [HttpGet("check-existence")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckExistence([FromQuery] Guid bookingId, [FromQuery] string guestEmail)
    {
        var result = await _mediator.Send(new CheckBookingExistenceForGuestQuery(bookingId, guestEmail));


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
        var result = await _mediator.Send(new CanBookRoomQuery(roomId, checkIn, checkOut));
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
    [HttpDelete("pending/{pendingBookingId}")]
    [Authorize]
    public async Task<IActionResult> DeletePendingBooking(Guid pendingBookingId)
    {
        var pendingBooking = await _pendingBookingRepository.GetPendingByIdAsync(pendingBookingId);
        if (pendingBooking == null)
            return NotFound("Pending booking not found.");

        var guest = await _pendingBookingRepository.GetByUserIdAsync(pendingBooking.UserId);
        if (guest != null)
        {
            var (subject, body) = _emailTemplate.GetEmailContent(PaymentStatus.Cancelled, guest.FirstName, pendingBooking.Room?.Hotel?.Name ?? "the hotel");
            await _emailSender.SendEmailAsync(guest.Email, subject, body);
        }

        await _pendingBookingRepository.DeleteAsync(pendingBookingId);
        return Ok("Pending booking has been cancelled.");
    }


}
