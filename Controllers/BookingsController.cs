using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Services.IService;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IConfiguration config, IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("available-tables")]
        public async Task<IActionResult> GetAvailableTables(DateTime date, int guests)
        {
            var availableTables = await _bookingService.GetAvailableTablesAsync(date, guests);
            return Ok(availableTables);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO booking)
        {
            var result = await _bookingService.CreateBookingAsync(booking);
            if (result)
                return Ok(new { message = "Booking created successfully" });
            else
                return BadRequest(new { message = "Failed to create booking" });
        }

        [HttpPost]
        [Route("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CustomerDTO customer)
        {
            var result = await _bookingService.CancelBookingAsync(customer);
            if (result)
                return Ok(new { message = "Cancelled successfully" });
            else
                return NotFound(new { message = "No booking found" });
        }

        //To be the Admin cancel booking based on any details
        [HttpPost]
        [Route("cancel-by-details")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelBookingByDetails(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null)
        {
            var result = await _bookingService.CancelBookingAsync(bookingID, tableID, CustomeerID, bookingDate);
            if (result)
                return Ok(new { message = "Booking cancelled successfully" });
            else
                return NotFound(new { message = "No booking found with the provided details" });
        }
    }
}
