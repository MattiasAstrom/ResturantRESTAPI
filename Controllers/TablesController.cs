using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public TablesController(IBookingService service)
        {
            _bookingService = service;
        }
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTables(DateTime startTime, int numberOfGuests)
        {
            var tables = await _bookingService.GetAvailableTablesAsync(startTime, numberOfGuests);
            if (tables == null || !tables.Any())
            {
                return NotFound("No available tables found.");
            }
            return Ok(tables);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] ResturantRESTAPI.Models.Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Invalid booking data.");
            }
            var result = await _bookingService.CreateBookingAsync(booking);
            if (!result)
            {
                return StatusCode(500, "Failed to create booking.");
            }
            return Ok("Booking created successfully.");
        }

        [HttpDelete("CustomerCancel")]
        public async Task<IActionResult> CancelBooking([FromBody] ResturantRESTAPI.Models.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid customer data.");
            }
            var result = await _bookingService.CancelBookingAsync(customer);
            if (!result)
            {
                return StatusCode(500, "Failed to cancel booking.");
            }
            return Ok("Booking cancelled successfully.");
        }

        [HttpDelete("AdminCancel")]
        public async Task<IActionResult> CancelBooking(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null)
        {
            if (bookingID == null && (tableID == 0 || CustomeerID == 0 || bookingDate == null))
            {
                return BadRequest("Invalid cancellation parameters.");
            }
            var result = await _bookingService.CancelBookingAsync(bookingID, tableID, CustomeerID, bookingDate);
            if (!result)
            {
                return StatusCode(500, "Failed to cancel booking.");
            }
            return Ok("Booking cancelled successfully.");
        }
    }
}
