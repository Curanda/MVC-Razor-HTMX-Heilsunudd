using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class BookingController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public BookingController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            return View(await _context.Booking.ToListAsync());
        }


        // GET: Booking/Create
        public IActionResult Create()
        {
            var services = _context.Service.ToList();
            var locations = _context.Location.ToList();
            var statuses = _context.Status.ToList();

            var booking = new Booking
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Email = "",
                IdService = services.FirstOrDefault()?.IdService ?? 1,
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                BookingTime = TimeOnly.FromDateTime(DateTime.Now),
                IdLocation = locations.FirstOrDefault()?.IdLocation ?? 1,
                IdStatus = statuses.FirstOrDefault()?.IdStatus ?? 1,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now)
            };


            // foreach (var item in services)
            // {
            //     Console.WriteLine($"servicename {item.ServiceName}, serviceid: {item.IdService}");
            // }

            // Console.WriteLine();
            ViewData["Service"] = services.ToDictionary(x => x.IdService, x => x.ServiceName);
            ViewData["Location"] = locations.ToDictionary(x => x.IdLocation, x => x.LocationName);
            ViewData["Status"] = statuses.ToDictionary(x => x.IdStatus, x => x.StatusName);
            // ViewBag.CreateParams = new Dictionary<string, int>();

            return View(booking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PhoneNumber,Email,Kennitala,IdService,BookingDate,BookingTime,IdLocation,IdStatus")] Booking booking)
        {
            booking.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

            foreach (var key in Request.Form.Keys)
            {
                if (key == "Service")
                {
                    booking.IdService = int.Parse(Request.Form[key]);
                }

                if (key == "Location")
                {
                    booking.IdLocation = int.Parse(Request.Form[key]);
                }

                if (key == "Status")
                {
                    booking.IdStatus = int.Parse(Request.Form[key]);
                }
            }

            var services = await _context.Service.ToListAsync();
            var locations = await _context.Location.ToListAsync();
            var statuses = await _context.Status.ToListAsync();

            booking.Service = services.FirstOrDefault(x => x.IdService == booking.IdService);
            booking.Location = locations.FirstOrDefault(x => x.IdLocation == booking.IdLocation);
            booking.Status = statuses.FirstOrDefault(x => x.IdStatus == booking.IdStatus);

            try
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save failed: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                ModelState.AddModelError("", $"Failed to save: {ex.Message}");

                ViewData["Service"] = services.ToDictionary(x => x.IdService, x => x.ServiceName);
                ViewData["Location"] = locations.ToDictionary(x => x.IdLocation, x => x.LocationName);
                ViewData["Status"] = statuses.ToDictionary(x => x.IdStatus, x => x.StatusName);
                return View(booking);
            }

        }
        



// GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            var services = _context.Service.ToList();
            var locations = _context.Location.ToList();
            var statuses = _context.Status.ToList();
            
            ViewData["Service"] = services.ToDictionary(x => x.IdService, x => x.ServiceName);
            ViewData["Location"] = locations.ToDictionary(x => x.IdLocation, x => x.LocationName);
            ViewData["Status"] = statuses.ToDictionary(x => x.IdStatus, x => x.StatusName);
            
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBooking,FirstName,LastName,PhoneNumber,Email,Kennitala,IdService,BookingDate,BookingTime,IdLocation,IdStatus,CreatedDate")] Booking booking)
        {
            if (id != booking.IdBooking)
            {
                booking.IdBooking = id;
            }
            
            if (!ModelState.IsValid) 
            {
                ViewData["Service"] = _context.Service?.ToDictionary(x => x.IdService, x => x.ServiceName) 
                                      ?? new Dictionary<int, string>();
                ViewData["Location"] = _context.Location?.ToDictionary(x => x.IdLocation, x => x.LocationName) 
                                       ?? new Dictionary<int, string>();
                ViewData["Status"] = _context.Status?.ToDictionary(x => x.IdStatus, x => x.StatusName) 
                                     ?? new Dictionary<int, string>();
                return View(booking);
            }
            
            foreach (var key in Request.Form.Keys)
            {
                switch (key)
                {
                    case "Service":
                        booking.IdService = int.Parse(Request.Form[key]);
                        break;
                    case "Location":
                        booking.IdLocation = int.Parse(Request.Form[key]);
                        break;
                    case "Status":
                        booking.IdStatus = int.Parse(Request.Form[key]);
                        break;
                }
            }


            
            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.IdBooking))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Service)
                .Include(b => b.Location)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(m => m.IdBooking == id);
            
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.IdBooking == id);
        }
    }
}