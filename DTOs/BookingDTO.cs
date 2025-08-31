namespace ResturantRESTAPI.DTOs
{
    public class BookingDTO
    {
        public int FK_Table { get; set; }
        public int FK_Customer { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
