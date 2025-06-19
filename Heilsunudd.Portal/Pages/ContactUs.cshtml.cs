using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Heilsunudd.Pages;

public class ContactUs(HeilsunuddDbContext context) : PageModel
{
    private HeilsunuddDbContext _context = context;
    private CustomMessage _customMessage = new CustomMessage
    {
        Name = "",
        Email = "",
        Message = ""
    };
    public void OnGet()
    {
    }
    
    public async Task<IActionResult> OnPostCustomMessage(string name, string email, string message)
    {
        var customMessage = new CustomMessage
        {
            Name = name,
            Email = email,
            Message = message
        };
    
        Console.WriteLine($"Name: {customMessage.Name}, Email: {customMessage.Email}, Message: {customMessage.Message}");
    
        try
        {
            _context.CustomMessage.Add(customMessage);
            await _context.SaveChangesAsync();
            return Content(@"
            <div class='p-4 mb-4 text-sm text-green-800 border border-green-300 rounded-lg bg-green-50'>
                ✅ Message sent! We'll be in touch.
            </div>
            <script>
                document.querySelector('form').reset();
                setTimeout(() => {
                    document.querySelector('.text-green-800').style.display = 'none';
                }, 5000);
            </script>");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Content(@"<div class='p-4 mb-4 text-sm text-red-800 border border-red-300 rounded-lg bg-red-50'>
                ❌ Connection error. Please try again.
                </div>");
        }
    }
    
    
}