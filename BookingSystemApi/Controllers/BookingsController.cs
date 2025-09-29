using BookingSystemApi.Models;
using BookingSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var booking = await _bookingService.AddBookingAsync(request);
            return Ok(booking);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Booking>> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            return booking;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteBooking(Guid id)
        {
            return await _bookingService.DeleteBookingAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult<Booking>> UpdateBooking(Guid id, [FromBody] UpdateBookingRequest request)
        {
            var booking = await _bookingService.UpdateBookingAsync(id, request);
            return Ok(booking);
        }
    }
}