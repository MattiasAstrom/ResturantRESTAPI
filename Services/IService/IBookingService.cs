using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Services.IService
{
    public interface IBookingService
    {
        Task<IEnumerable<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests);
        Task<bool> CreateBookingAsync(BookingDTO booking);
        Task<bool> CancelBookingAsync(CustomerDTO customer);
        Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null);
    }
}
