using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class ServiceController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public ServiceController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Service.ToListAsync());
        }

        // // GET: Services/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var Service = await _context.Service
        //         .FirstOrDefaultAsync(m => m.IdService == id);
        //     if (Service == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(Service);
        // }

        // GET: Service/Create
        public async Task<IActionResult> Create()
        {
            var Service = new Service
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
            
            return View(Service);
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdService,ServiceName,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] Service service, int[]? selectedLocations)
        {
            if (ModelState.IsValid)
            {

                _context.Add(service);
                await _context.SaveChangesAsync();
        
             
                if (selectedLocations != null && selectedLocations.Length != 0)
                {
                    var locations = await _context.Location
                        .Where(l => selectedLocations.Contains(l.IdLocation))
                        .ToListAsync();
                
                    foreach (var location in locations)
                    {
                        service.Locations.Add(location);
                    }
            
                    await _context.SaveChangesAsync();
                }
        
                return RedirectToAction(nameof(Index));
            }
    
            ViewBag.SelectedLocations = new List<int>();
            ViewBag.Locations = await _context.Location.Where(l=>l.LocationIsActive).ToListAsync();
    
            return View(service);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            Console.WriteLine("Edit Service hit");
            
            if (id == null)
            {
                Console.WriteLine("id not found");
                
                return NotFound();
            }

            var Service = await _context.Service
                .Include(s => s.Locations)
                .FirstOrDefaultAsync(s => s.IdService == id);
    
            if (Service == null)
            {
                return NotFound();
            }
    
            ViewBag.Locations = await _context.Location
                .Where(l => l.LocationIsActive)
                .ToListAsync();
            
            ViewBag.SelectedLocations = Service.Locations.Select(l => l.IdLocation).ToList();
    
            return View(Service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdService,ServiceName,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] Service service, int[]? selectedLocations)
        {
            if (id != service.IdService)
            {
                service.IdService = id;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingService = await _context.Service
                        .Include(s => s.Locations)
                        .FirstOrDefaultAsync(s => s.IdService == id);

                    if (existingService != null)
                    {
                        _context.Entry(existingService).CurrentValues.SetValues(service);
                        
                        existingService.Locations.Clear();
                        
                        if (selectedLocations?.Length > 0)
                        {
                            var locations = await _context.Location
                                .Where(l => selectedLocations.Contains(l.IdLocation))
                                .ToListAsync();
                    
                            foreach (var location in locations)
                            {
                                existingService.Locations.Add(location);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.IdService))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
    
            ViewBag.SelectedLocations = selectedLocations?.ToList() ?? new List<int>();
            ViewBag.Locations = await _context.Location.Where(l=>l.LocationIsActive).ToListAsync();
    
            return View(service);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var Service = await _context.Service
                .FirstOrDefaultAsync(m => m.IdService == id);
            if (Service == null)
            {
                return NotFound();
            }
        
            return View(Service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Service = await _context.Service.FindAsync(id);
            if (Service != null)
            {
                _context.Service.Remove(Service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.IdService == id);
        }
    }
}