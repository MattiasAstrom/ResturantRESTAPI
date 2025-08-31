using ResturantRESTAPI.Models;

namespace RestaurantBookingAPI.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests);
        Task<bool> CreateBookingAsync(Booking booking);
    }
}
