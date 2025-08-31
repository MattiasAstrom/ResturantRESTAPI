using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IBookingService _bookingService;

        public BookingsController(IConfiguration config, IBookingService bookingService)
        {
            _config = config;
            _bookingService = bookingService;
        }

        [HttpGet("available-tables")]
        public async Task<IActionResult> GetAvailableTables(DateTime date, int guests)
        {
            var availableTables = await _bookingService.GetAvailableTablesAsync(date, guests);
            return Ok(availableTables);
        }
    }
}
