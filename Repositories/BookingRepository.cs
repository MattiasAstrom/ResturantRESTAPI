using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.DTOs;
using ResturantRESTAPI.Migrations;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Repositories.IRepositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == booking.CustomerPhoneNumber);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = booking.CustomerName,
                    PhoneNumber = booking.CustomerPhoneNumber
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var newBooking = new Booking
            {
                FK_Table = booking.TableID,
                FK_Customer = customer.Id,
                StartTime = booking.StartTime,
                NumberOfGuests = booking.NumberOfGuests
            };

            _context.Add(newBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int numberOfGuests)
        {
            await UpdateTableOccupancy();

            //fit the number of guests
            var tables = await _context.Tables
                .Where(t => t.Capacity >= numberOfGuests)
                .ToListAsync();

            //grab overlapping bookings
            var bookedTableIds = await _context.Bookings
                .Where(b => b.StartTime.AddHours(-2) < startTime && startTime < b.StartTime.AddHours(2))
                .Select(b => b.FK_Table)
                .ToListAsync();

            //remove overlap
            var availableTables = tables
                .Where(t => !bookedTableIds.Contains(t.Id))
                .ToList();

            //manual map to DTO
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

        private async Task UpdateTableOccupancy()
        {
            var currentTime = DateTime.Now;
            double cleanUpDbTableInterval = 10; 

            var oldBookings = _context.Bookings.Where(b => b.StartTime.AddHours(cleanUpDbTableInterval) < currentTime);
            _context.Bookings.RemoveRange(oldBookings);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CancelBookingAsync(CustomerDTO customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.PhoneNumber)) //phonenumber should be a required field
                return false;
            
            var booking = await _context.Bookings.Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Customer.PhoneNumber == customer.PhoneNumber);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        //admin cancellation based on any parameter
        public async Task<bool> CancelBookingAsync(int? bookingID = null, int? tableID = 0, int? customerID = 0, DateTime? bookingDate = null)
        {
            //check if any parameter is provided
            if (bookingID == null && tableID == 0 && customerID == 0 && bookingDate == null)
                return false;

            //remove the booking based on the provided parameter
            var bookingQuery = _context.Bookings.AsQueryable();

            if (bookingID != null)
                bookingQuery = bookingQuery.Where(b => b.Id == bookingID);
            if (tableID != 0)
                bookingQuery = bookingQuery.Where(b => b.FK_Table == tableID);
            if (customerID != 0)
                bookingQuery = bookingQuery.Where(b => b.FK_Customer == customerID);
            if (bookingDate != null)
                bookingQuery = bookingQuery.Where(b => b.StartTime == bookingDate);

            var bookingsToRemove = await bookingQuery.ToListAsync();
            if (!bookingsToRemove.Any())
                return false;

            _context.Bookings.RemoveRange(bookingsToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
