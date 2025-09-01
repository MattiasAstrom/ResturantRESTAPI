namespace ResturantRESTAPI.DTOs
{
    public class BookingDTO
    {
        public int TableID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
