using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests);
        
        Task<bool> CreateBookingAsync(Booking booking);

        Task<bool> CancelBookingAsync(Customer customer);
        Task<bool> CancelBookingAsync(Booking booking);
    }
}
