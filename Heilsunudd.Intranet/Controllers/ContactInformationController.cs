using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Intranet.Data;
using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class ContactInformationController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public ContactInformationController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: ContactInformation
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactInformation.ToListAsync());
        }

        // GET: ContactInformation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation
                .FirstOrDefaultAsync(m => m.IdContactInformation == id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // GET: ContactInformation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactInformation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContactInformation,PhoneNumber,Kennitala,CompanyName")] ContactInformation contactInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactInformation);
        }

        // GET: ContactInformation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation.FindAsync(id);
            if (contactInformation == null)
            {
                return NotFound();
            }
            return View(contactInformation);
        }

        // POST: ContactInformation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContactInformation,PhoneNumber,Kennitala,CompanyName")] ContactInformation contactInformation)
        {
            if (id != contactInformation.IdContactInformation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInformationExists(contactInformation.IdContactInformation))
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
            return View(contactInformation);
        }

        // GET: ContactInformation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation
                .FirstOrDefaultAsync(m => m.IdContactInformation == id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // POST: ContactInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactInformation = await _context.ContactInformation.FindAsync(id);
            if (contactInformation != null)
            {
                _context.ContactInformation.Remove(contactInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactInformationExists(int id)
        {
            return _context.ContactInformation.Any(e => e.IdContactInformation == id);
        }
    }
}
