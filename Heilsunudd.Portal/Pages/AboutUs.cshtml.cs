using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heilsunudd.Pages;

public class AboutUsModel(ILogger<AboutUsModel> logger, HeilsunuddDbContext context) : PageModel
{
    private readonly ILogger<AboutUsModel> _logger = logger;
    public IList<AboutUs> AboutUsItems { get; set; } = new List<AboutUs>();

    public Task OnGetAsync()
    {
        AboutUsItems = context.AboutUs.ToList();
        return Task.CompletedTask;
    }
    
}