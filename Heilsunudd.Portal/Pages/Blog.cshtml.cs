using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Pages;

public class Blog(HeilsunuddDbContext context) : PageModel
{
    private HeilsunuddDbContext _context = context;
    public ICollection<BlogCategory> BlogCategories {get;set;} = new List<BlogCategory>();
    public ICollection<BlogPost> BlogPosts {get;set;} = new List<BlogPost>();
    
    public async Task OnGetAsync()
    {
        BlogPosts = await _context.BlogPost
            .Include(bp => bp.BlogCategory)
            .OrderByDescending(bp => bp.PublicationDate)
            .ToListAsync();
        
        BlogCategories = await _context.BlogCategory.ToListAsync();
    }
    
    public IEnumerable<string> GetTags(BlogPost? blogPost)
    {
        if (string.IsNullOrWhiteSpace(blogPost?.Tags))
            return [];

        return blogPost.Tags
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(tag => tag.Trim())
            .Where(tag => !string.IsNullOrEmpty(tag));
    }

    public async Task<IActionResult> OnGetChosenBlogPost(int id)
    {
        var blogpost = await _context.BlogPost.Include(bp => bp.BlogCategory).FirstOrDefaultAsync(bp => bp.IdBlogPost == id);
        if (blogpost == null) return NotFound();
        ViewData["Tags"] = GetTags(blogpost);
        
        return Partial("_BlogPostPartial", blogpost);
    }
    
    public async Task<IActionResult> OnGetSearchBlogPosts(string searchTerm)
    {
        Console.WriteLine("Search term: " + searchTerm + "");
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Content("", "text/html");
        }

        var searchResults = await _context.BlogPost
            .Include(bp => bp.BlogCategory)
            .Where(bp => bp.BlogCategory != null && (bp.Title.Contains(searchTerm) || 
                                                     bp.Content.Contains(searchTerm) || 
                                                     bp.Tags.Contains(searchTerm) ||
                                                     bp.BlogCategory.CategoryName.Contains(searchTerm)))
            .OrderByDescending(bp => bp.PublicationDate)
            .Take(5)
            .ToListAsync();

        if (searchResults.Count == 0)
        {
            return Content("<li class='text-center py-4 text-gray-500'>No search results</li>", "text/html");
        }

        var html = searchResults.Aggregate("", (current, post) => current + $"<li>\n<a hx-target=\"#blogPost\" hx-swap=\"innerHtml\" hx-get=\"/Blog?handler=ChosenBlogPost&id={post.IdBlogPost}\" class=\"flex items-center w-full p-2 text-gray-900 transition duration-75 rounded-lg pl-11 group hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700\">{post.Title}</a>\n</li>");


        return Content(html,"text/html");
    }
}