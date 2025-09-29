using BookingSystemApi.Models;
using BookingSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpPost("bookings")]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var booking = await _bookingService.AddBookingAsync(request);
            return Ok(booking);
        }

        [HttpGet("bookings/{id:guid}")]
        public async Task<ActionResult<Booking>> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            return booking;
        }

        [HttpDelete("bookings/{id:guid}/delete")]
        public async Task<ActionResult<bool>> DeleteBooking(Guid id)
        {
            return await _bookingService.DeleteBookingAsync(id);
        }

        [HttpPut("bookings/{id:guid}/update")]
        public async Task<ActionResult<Booking>> UpdateBooking(Guid id, [FromBody] UpdateBookingRequest request)
        {
            var booking = await _bookingService.UpdateBookingAsync(id, request);
            return Ok(booking);
        }

        [HttpGet("bookings")]
        public async Task<ActionResult<IReadOnlyList<Booking>>> GetBookings()
        {
            var bookings = await _bookingService.GetBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("customers/{id:guid}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _bookingService.GetCustomerAsync(id);
            return customer;
        }

        [HttpGet("customers")]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var customer = await _bookingService.GetCustomersAsync();
            return Ok(customer);
        }
    }
}