using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heilsunudd.Pages;

public class BookingsModel : PageModel
{
    private readonly ILogger<BookingsModel> _logger;

     

    public BookingsModel(ILogger<BookingsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

        public IActionResult OnGetHello()
    {
        return Content("<strong>Hello, htmx!</strong>", "text/html");
    }
}