using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Services.IService
{
    public interface IBookingService
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests);
        Task<bool> CreateBookingAsync(Booking booking);
        Task<bool> CancelBookingAsync(Customer customer);
        Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null);
    }
}
