namespace EventEasePoe.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public String VenueName { get; set; }
        public String Location { get; set; }
        public int Capacity { get; set; }
        public String ImageURL { get; set; }
        public List<Booking> Booking { get; set; } = new();
    }
}
