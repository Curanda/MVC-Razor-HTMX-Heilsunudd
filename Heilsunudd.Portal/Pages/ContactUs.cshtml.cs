using Heilsunudd.Data.Data.CMS;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    
    public async Task<IActionResult> OnPostCustomMessage(string name, string email, string message, string? idBooking)
    {
        var customMessage = new CustomMessage
        {
            Name = name,
            Email = email,
            Message = message
        };

        Dictionary<string, string> messageFeedback = new()
        {
            { "delete", """

                                    <div class='p-4 mb-4 text-sm text-green-800 border border-green-300 rounded-lg bg-green-50'>
                                        ✅ Booking successfully deleted.
                                    </div>
                                    <script>
                                        document.querySelector('form').reset();
                                        setTimeout(() => {
                                            document.querySelector('.text-green-800').style.display = 'none';
                                        }, 5000);
                                        document.getElementById("idBooking").classList.add("hidden");
                                        document.getElementById("idBooking").removeAttribute("required");
                                    </script>
                        """ },
            { "wrongId", """
                         <div class='p-4 mb-4 text-sm text-red-800 border border-red-300 rounded-lg bg-red-50'>
                                         ❌ Wrong booking Id or connection error.
                                         </div>
                         """ },
            { "success", """

                                     <div class='p-4 mb-4 text-sm text-green-800 border border-green-300 rounded-lg bg-green-50'>
                                         ✅ Message sent! We'll be in touch.
                                     </div>
                                     <script>
                                         document.querySelector('form').reset();
                                         setTimeout(() => {
                                             document.querySelector('.text-green-800').style.display = 'none';
                                         }, 5000);
                                     </script>
                         """ },
            {"fail","""

                                <div class='p-4 mb-4 text-sm text-green-800 border border-green-300 rounded-lg bg-green-50'>
                                    ❌ Failed to sent message. Try again.
                                </div>
                                <script>
                                    document.querySelector('form').reset();
                                    setTimeout(() => {
                                        document.querySelector('.text-green-800').style.display = 'none';
                                    }, 5000);
                                </script>
                    """}
        };

        var setMessageFeedback = "";
    
        if (idBooking is not null && idBooking.Length > 0 && int.TryParse(idBooking, out var idBookingParsed))
        {
            try
            {
                var bookingForDeletion = await _context.Booking.FirstOrDefaultAsync(p => p.IdBooking == idBookingParsed && p.Email == email);
                _context.Booking.Remove(bookingForDeletion!);
                customMessage.Message += $"\n\nDeleted:{idBooking}";
                setMessageFeedback = "delete";
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content(messageFeedback["wrongId"]);
            }
        }
        
        Console.WriteLine($"Name: {customMessage.Name}, Email: {customMessage.Email}, Message: {customMessage.Message}");
    
        try
        {
            _context.CustomMessage.Add(customMessage);
            await _context.SaveChangesAsync();
            return Content(setMessageFeedback.Length > 0 ? messageFeedback[setMessageFeedback] : messageFeedback["success"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Content(messageFeedback["fail"]);
        }
    }
    
    
}