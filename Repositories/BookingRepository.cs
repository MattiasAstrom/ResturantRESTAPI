using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.DTOs;
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

        public async Task<bool> CreateBookingAsync(BookingDTO booking)
        {
            if (booking == null)
                return false;
            
            var newCustomer = new Customer
            {
                Name = booking.CustomerName,
                PhoneNumber = booking.CustomerPhoneNumber
            };

            _context.Add(newCustomer);
            await _context.SaveChangesAsync();

            var newBooking = new Booking
            {
                FK_Table = booking.TableID,
                FK_Customer = newCustomer.Id,
                StartTime = booking.StartTime,
                NumberOfGuests = booking.NumberOfGuests
            };

            _context.Add(newBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
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

            //map to DTO
            var availableTableDTOs = availableTables
                .Select(t => new TableDTO
                {
                    TableNumber = t.TableNumber,
                    Capacity = t.Capacity,
                    IsOccupied = t.IsOccupied
                })
                .ToList();

            return availableTableDTOs;
        }

        public async Task<bool> CancelBookingAsync(CustomerDTO customer)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(t => t.Customer.PhoneNumber == customer.PhoneNumber);
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
