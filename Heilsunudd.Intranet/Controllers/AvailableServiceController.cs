using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class AvailableServiceController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public AvailableServiceController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: AvailableServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.AvailableService.ToListAsync());
        }

        // GET: AvailableServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableService = await _context.AvailableService
                .FirstOrDefaultAsync(m => m.IdService == id);
            if (availableService == null)
            {
                return NotFound();
            }

            return View(availableService);
        }

        // GET: AvailableService/Create
        public async Task<IActionResult> Create()
        {
            var availableService = new AvailableService
            {
                ServiceName = string.Empty,
                ServiceDuration = 0,
                ServicePrice = 0.0m,
                ServiceDescription = string.Empty,
                ServiceImageUrl = string.Empty,
                ServiceIsActive = true
            };
            
            ViewBag.SelectedLocations = new List<int>();

            ViewBag.Locations = await _context.Location.Where(l=>l.LocationIsActive).ToListAsync();
            
            return View(availableService);
        }

        // POST: AvailableService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdService,ServiceName,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] AvailableService availableService, int[]? selectedLocations)
        {
            if (ModelState.IsValid)
            {
                if (selectedLocations != null && selectedLocations.Length != 0)
                {
                    var locations = await _context.Location.Where(l => selectedLocations.Contains(l.IdLocation)).ToListAsync();
                    availableService.Locations = locations;
                }
                _context.Add(availableService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            
            ViewBag.SelectedLocations = new List<int>();

            ViewBag.Locations = await _context.Location.Where(l=>l.LocationIsActive).ToListAsync();
            
            return View(availableService);
        }

        // GET: AvailableService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableService = await _context.AvailableService
                .Include(s => s.Locations)
                .FirstOrDefaultAsync(s => s.IdService == id);
    
            if (availableService == null)
            {
                return NotFound();
            }
    
            ViewBag.Locations = await _context.Location
                .Where(l => l.LocationIsActive)
                .ToListAsync();
            
            ViewBag.SelectedLocations = availableService.Locations.Select(l => l.IdLocation).ToList();
    
            return View(availableService);
        }

        // POST: AvailableService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdService,ServiceName,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] AvailableService availableService,  int[]? selectedLocations)
        {
            if (id != availableService.IdService)
            {
                availableService.IdService = id;
            }

            if (!ModelState.IsValid) return View(availableService);
            try
            {
                if (selectedLocations?.Length != 0)
                {
                    availableService.Locations = await _context.Location.Where(l => selectedLocations != null && selectedLocations.Contains(l.IdLocation)).ToListAsync();
                }
                
                _context.Update(availableService);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (AvailableServiceExists(availableService.IdService))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AvailableService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableService = await _context.AvailableService
                .FirstOrDefaultAsync(m => m.IdService == id);
            if (availableService == null)
            {
                return NotFound();
            }

            return View(availableService);
        }

        // POST: AvailableService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableService = await _context.AvailableService.FindAsync(id);
            if (availableService != null)
            {
                _context.AvailableService.Remove(availableService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableServiceExists(int id)
        {
            return _context.AvailableService.Any(e => e.IdService == id);
        }
    }
}