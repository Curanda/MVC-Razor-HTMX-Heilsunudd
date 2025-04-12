using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;

namespace Heilsunudd.Intranet.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly HeilsunuddDbContext _context;

        public BlogPostController(HeilsunuddDbContext context)
        {
            _context = context;
        }

        // GET: BlogPost
        public async Task<IActionResult> Index()
        {
            var HeilsunuddDbContext = _context.BlogPost.Include(b => b.BlogCategory);
            return View(await HeilsunuddDbContext.ToListAsync());
        }

        // GET: BlogPost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .Include(b => b.BlogCategory)
                .FirstOrDefaultAsync(m => m.IdBlogPost == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: BlogPost/Create
        public IActionResult Create()
        {
            ViewData["IdBlogCategory"] = new SelectList(_context.Set<BlogCategory>(), "IdBlogCategory", "CategoryName");
            return View();
        }

        // POST: BlogPost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBlogPost,Title,Content,ImageUrl,AuthorName,PublicationDate,IdBlogCategory,Tags")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBlogCategory"] = new SelectList(_context.Set<BlogCategory>(), "IdBlogCategory", "CategoryName", blogPost.IdBlogCategory);
            return View(blogPost);
        }

        // GET: BlogPost/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["IdBlogCategory"] = new SelectList(_context.Set<BlogCategory>(), "IdBlogCategory", "CategoryName", blogPost.IdBlogCategory);
            return View(blogPost);
        }

        // POST: BlogPost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBlogPost,Title,Content,ImageUrl,AuthorName,PublicationDate,IdBlogCategory,Tags")] BlogPost blogPost)
        {
            if (id != blogPost.IdBlogPost)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.IdBlogPost))
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
            ViewData["IdBlogCategory"] = new SelectList(_context.Set<BlogCategory>(), "IdBlogCategory", "CategoryName", blogPost.IdBlogCategory);
            return View(blogPost);
        }

        // GET: BlogPost/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .Include(b => b.BlogCategory)
                .FirstOrDefaultAsync(m => m.IdBlogPost == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPost.Remove(blogPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPost.Any(e => e.IdBlogPost == id);
        }
    }
}
