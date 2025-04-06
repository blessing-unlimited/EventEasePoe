using EventEasePoe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEasePoe.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventEaseDbContext _context;
        public BookingController(EventEaseDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var booking = await _context.Booking.ToListAsync();
            return View(booking);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var events = _context.Event.ToList();
            var venues = _context.Venue.ToList();

            ViewBag.Event = new SelectList(events, "EventID", "EventName");
            ViewBag.Venue = new SelectList(venues, "VenueID", "VenueName");

            return View();
        }

        //(Troelsen and Japikse,2022)
        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var events = _context.Event.ToList();
            var venues = _context.Venue.ToList();

            ViewBag.Event = new SelectList(events, "EventID", "EventName");
            ViewBag.Venue = new SelectList(venues, "VenueID", "VenueName");

            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookings = await _context.Booking.FirstOrDefaultAsync(m => m.BookingID == id);

            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bookings = await _context.Booking.FindAsync(id);

            if (bookings == null)
            {
                return NotFound();
            }
            _context.Booking.Remove(bookings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

    }
}
