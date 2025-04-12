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
    public class AvailableServicesController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public AvailableServicesController(HeilsunuddDbContext context)
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

        // GET: AvailableServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailableServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdService,ServiceType,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] AvailableService availableService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(availableService);
        }

        // GET: AvailableServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableService = await _context.AvailableService.FindAsync(id);
            if (availableService == null)
            {
                return NotFound();
            }
            return View(availableService);
        }

        // POST: AvailableServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdService,ServiceType,ServiceDuration,ServicePrice,ServiceDescription,ServiceImageUrl,ServiceIsActive")] AvailableService availableService)
        {
            if (id != availableService.IdService)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableServiceExists(availableService.IdService))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(availableService);
        }

        // GET: AvailableServices/Delete/5
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

        // POST: AvailableServices/Delete/5
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
