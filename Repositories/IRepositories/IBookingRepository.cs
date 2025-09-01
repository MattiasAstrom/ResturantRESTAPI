using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<List<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests);

        Task<bool> CreateBookingAsync(Booking booking);
        Task<bool> CancelBookingAsync(Customer customer);
        Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null);
    }
}
