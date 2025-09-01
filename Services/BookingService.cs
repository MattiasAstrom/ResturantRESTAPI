using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;
using ResturantRESTAPI.Services.IService;

namespace RestaurantBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<IEnumerable<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
        {
            return await _bookingRepo.GetAvailableTablesAsync(startTime, numberOfGuests);
        }

        public async Task<bool> CreateBookingAsync(BookingDTO booking)
        {
            return await _bookingRepo.CreateBookingAsync(booking);
        }
        
        public async Task<bool> CancelBookingAsync(CustomerDTO customer)
        {
            return await _bookingRepo.CancelBookingAsync(customer);
        }

        public async Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null)
        {
            return await _bookingRepo.CancelBookingAsync(bookingID, tableID, CustomeerID, bookingDate);
        }
    }
}
