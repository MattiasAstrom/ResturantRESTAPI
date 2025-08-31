using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;

namespace RestaurantBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
        {
            return await _bookingRepo.GetAvailableTablesAsync(startTime, numberOfGuests);
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            return true;
        }
    }
}
