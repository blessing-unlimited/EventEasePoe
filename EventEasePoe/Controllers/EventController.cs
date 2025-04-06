using EventEasePoe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace EventEasePoe.Controllers
{

    public class EventController : Controller
    {
        private readonly EventEaseDbContext _context;
        public EventController(EventEaseDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _context.Event.ToListAsync();
            return View(events);
        }

        //(Troelsen and Japikse,2022)
        [HttpGet]
        public IActionResult Create()
        {
            var venues = _context.Venue.ToList();
            ViewBag.Venue = new SelectList(venues, "VenueID", "VenueName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var venues = _context.Venue.ToList();
            ViewBag.Venue = new SelectList(venues, "VenueID", "VenueName");
            return View(newEvent);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var events = await _context.Event.FirstOrDefaultAsync(m => m.EventID == id);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var events = await _context.Event.FindAsync(id);
            _context.Event.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventID == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Event.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event events)
        {
            if (id != events.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(events.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(events);

        }
    }
}
