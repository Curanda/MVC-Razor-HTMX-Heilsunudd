using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class LocationController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public LocationController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            return View(await _context.Location.ToListAsync());
        }
        
        
        public async Task<IActionResult> Create()
        {
            var location = new Location
            {
                LocationName = string.Empty,
                LocationTown = string.Empty,
                LocationStreet = string.Empty,
                LocationHouseNumber = string.Empty,
                LocationCoordinates = string.Empty,
                LocationDescription = string.Empty,
                LocationImageUrl = string.Empty,
                LocationIsActive = true
            };

            ViewBag.SelectedServices = new List<int>();
            
            ViewBag.AvailableServices = await _context.AvailableService
                .Where(s => s.ServiceIsActive)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();
    
            return View(location);
        }

        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLocation,LocationName,LocationTown,LocationStreet,LocationHouseNumber,LocationAdditionalInfo,LocationCoordinates,LocationDescription,LocationImageUrl,LocationIsActive")] Location location, int[]? selectedServices)
        {
            if (ModelState.IsValid)
            {
                if (selectedServices is { Length: > 0 })
                {
                    var services = await _context.AvailableService
                        .Where(s => selectedServices.Contains(s.IdService))
                        .ToListAsync();
            
                    location.AvailableServices = services;
                }

                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.SelectedServices = new List<int>();
            ViewBag.AvailableServices = await _context.AvailableService
                .Where(s => s.ServiceIsActive)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();
    
            return View(location);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var location = await _context.Location
                .Include(l => l.AvailableServices)
                .FirstOrDefaultAsync(l => l.IdLocation == id);
        
            if (location == null) return NotFound();

            ViewBag.AvailableServices = await _context.AvailableService
                .Where(s => s.ServiceIsActive)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();
    
            ViewBag.SelectedServices = location.AvailableServices.Select(s => s.IdService).ToList();
    
            return View(location);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLocation,LocationName,LocationTown,LocationStreet,LocationHouseNumber,LocationAdditionalInfo,LocationCoordinates,LocationDescription,LocationImageUrl,LocationIsActive")] Location location, int[]? selectedServices)
        {
            if (id != location.IdLocation)
            {
                location.IdLocation = id;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingLocation = await _context.Location
                        .Include(l => l.AvailableServices)
                        .FirstOrDefaultAsync(l => l.IdLocation == id);

                    if (existingLocation != null)
                    {
                        _context.Entry(existingLocation).CurrentValues.SetValues(location);
                        existingLocation.AvailableServices.Clear();
                        if (selectedServices is { Length: > 0 })
                        {
                            var services = await _context.AvailableService
                                .Where(s => selectedServices.Contains(s.IdService))
                                .ToListAsync();
                            
                            foreach (var service in services)
                            {
                                existingLocation.AvailableServices.Add(service);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.IdLocation))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.AvailableServices = await _context.AvailableService
                .Where(s => s.ServiceIsActive)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();
                
            return View(location);
        }



        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.IdLocation == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location != null)
            {
                _context.Location.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.IdLocation == id);
        }
    }
}