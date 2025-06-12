using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Heilsunudd.Intranet.Controllers
{
    public class CalendarController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public CalendarController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: Calendar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calendar.ToListAsync());
        }

        // GET: Calendar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .FirstOrDefaultAsync(m => m.IdCalendar == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // GET: Calendar/Create
        public IActionResult Create()
        {
            var bookings = _context.Booking.ToList();
            var locations = _context.Location.ToList();
            var statuses = _context.Status.ToList();
            
            var calendar = new Calendar
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                IdBooking = 0,
                IdLocation = 1,
                IdStatus = 1 
            };
            
            ViewData["Booking"] = bookings.ToDictionary(x => x.IdBooking, x => x.IdBooking.ToString());
            ViewData["Location"] = locations.ToDictionary(x => x.IdLocation, x => x.LocationName);
            ViewData["Status"] = statuses.ToDictionary(x => x.IdStatus, x => x.StatusName);
    
            return View(calendar);
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,IdBooking,IdLocation,IdStatus")] Calendar calendar)
        {
            
            foreach (var key in Request.Form.Keys)
            {
                switch (key)
                {
                    case "Booking":
                        calendar.IdBooking = int.Parse(Request.Form[key]);
                        break;
                    case "Location":
                        calendar.IdLocation = int.Parse(Request.Form[key]);
                        break;
                    case "Status":
                        calendar.IdStatus = int.Parse(Request.Form[key]);
                        break;
                }
            }

            var allBookings = await _context.Booking.ToListAsync();
            var allLocations = await _context.Location.ToListAsync();
            var allStatuses = await _context.Status.ToListAsync();

            calendar.Booking = allBookings.FirstOrDefault(x => x.IdService == calendar.IdBooking);
            calendar.Location = allLocations.FirstOrDefault(x => x.IdLocation == calendar.IdLocation);
            calendar.Status = allStatuses.FirstOrDefault(x => x.IdStatus == calendar.IdStatus);
            
            if (!ModelState.IsValid) return View(calendar);
            _context.Add(calendar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Calendar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }
            
            var bookings = _context.Booking.ToList();
            var locations = _context.Location.ToList();
            var statuses = _context.Status.ToList();
            
            ViewData["Booking"] = bookings.ToDictionary(x => x.IdBooking, x => x.IdBooking.ToString());
            ViewData["Location"] = locations.ToDictionary(x => x.IdLocation, x => x.LocationName);
            ViewData["Status"] = statuses.ToDictionary(x => x.IdStatus, x => x.StatusName);
            
            return View(calendar);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCalendar,StartTime,EndTime,IdBooking,IdLocation,IdStatus")] Calendar calendar)
        {
            if (id != calendar.IdCalendar)
            {
                calendar.IdCalendar = id;
                // return NotFound();
            }

            if (!ModelState.IsValid) return View(calendar);
            
            foreach (var key in Request.Form.Keys)
            {
                switch (key)
                {
                    case "Booking":
                        calendar.IdBooking = int.Parse(Request.Form[key]);
                        break;
                    case "Location":
                        calendar.IdLocation = int.Parse(Request.Form[key]);
                        break;
                    case "Status":
                        calendar.IdStatus = int.Parse(Request.Form[key]);
                        break;
                }
            }
            
            try
            {
                _context.Update(calendar);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(calendar.IdCalendar))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Calendar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .FirstOrDefaultAsync(m => m.IdCalendar == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar != null)
            {
                _context.Calendar.Remove(calendar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendar.Any(e => e.IdCalendar == id);
        }
    }
}