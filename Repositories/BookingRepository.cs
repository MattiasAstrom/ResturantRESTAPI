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

        public async Task<List<Table>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
        {
            //fit the number of guests
            var tables = _context.Tables
                .Where(t => t.Capacity >= numberOfGuests)
                .ToList();

            //grab overlapping bookings
            var bookedTableIds = _context.Bookings
                .Where(b => b.StartTime <= startTime.AddHours(-2) && startTime.AddHours(2) >= startTime)
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
            var booking = _context.Bookings.FirstOrDefault(t => t.FK_Customer == customer.Id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        //admin cancellation based on any parameter
        public async Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? CustomeerID = 0, DateTime? bookingDate = null)
        {
            //check if any parameter is provided
            if (bookingID == null && tableID == 0 && CustomeerID == 0 && bookingDate == null)
                return false;

            //remove the booking based on the provided parameter
            var booking = new Booking();
            if (bookingID != null)
                booking = _context.Bookings.FirstOrDefault(t => t.Id == bookingID);
            else if (tableID != 0)
                booking = _context.Bookings.FirstOrDefault(t => t.FK_Table == tableID);
            else if (CustomeerID != 0)
                booking = _context.Bookings.FirstOrDefault(t => t.FK_Customer == CustomeerID);
            else if (bookingDate != null)
                booking = _context.Bookings.FirstOrDefault(t => t.StartTime == bookingDate);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
