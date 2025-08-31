using ResturantRESTAPI.Data;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;

namespace ResturantRESTAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ResturantDbContext _context;
        public BookingRepository(ResturantDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
        {
            //grab all tables that can fit the number of guests
            var tables =  _context.Tables
                .Where(t => t.Capacity >= numberOfGuests)
                .ToList();

            //grab overlapping bookings
            var bookedTableIds = _context.Bookings
                .Where(b => b.StartTime <= startTime && startTime.AddHours(2) >= startTime)
                .Select(b => b.FK_Table)
                .ToList();

            //remove overlap
            var availableTables = tables
                .Where(t => !bookedTableIds.Contains(t.Id))
                .ToList();
            
            return availableTables;
        }

        public async Task<bool> CancelBookingAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CancelBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

    }
}
