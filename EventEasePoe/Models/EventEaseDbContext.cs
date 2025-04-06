using Microsoft.EntityFrameworkCore;

namespace EventEasePoe.Models
{
    public class EventEaseDbContext : DbContext 
    {

        //(Troelsen and Japikse,2022)
        public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options) : base(options)
        {

        }

        public DbSet<Venue> Venue { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}
