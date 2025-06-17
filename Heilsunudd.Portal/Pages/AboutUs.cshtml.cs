using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Pages;

public class AboutUsModel(ILogger<AboutUsModel> logger, HeilsunuddDbContext context) : PageModel
{
    private readonly ILogger<AboutUsModel> _logger = logger;
    public IList<AboutUs> AboutUsItems { get; set; } = new List<AboutUs>();

    public async Task OnGet()
    {
        AboutUsItems = await context.AboutUs.ToListAsync();
    }
    
}