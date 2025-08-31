namespace ResturantRESTAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfGuests { get; set; }

        public int FK_Table { get; set; }
        public Table Table { get; set; }

        public int FK_Customer { get; set; }
        public Customer Customer { get; set; }
    }
}
