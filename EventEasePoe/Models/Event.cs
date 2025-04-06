namespace EventEasePoe.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public String EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public String Description { get; set; }

        public int VenueID { get; set; }

        public List<Booking> Booking { get; set; } = new();
    }
}
