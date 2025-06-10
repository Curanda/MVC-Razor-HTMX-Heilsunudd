using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.EntityFrameworkCore;


namespace Heilsunudd.Pages;

public class BookingsModel(ILogger<BookingsModel> logger, HeilsunuddDbContext context) : PageModel
{
    private readonly ILogger<BookingsModel> _logger = logger;
    public IList<Location> Locations { get;set; } = new List<Location>();
    public IList<Calendar> CalendarRecords { get;set; } = new List<Calendar>();
    public IList<AvailableService> AvailableServices { get;set; } = new List<AvailableService>();
    public IList<Status> Statuses { get;set; } = new List<Status>();
    public IList<Calendar> ExistingCalendarEntries { get;set; } = new List<Calendar>();


    public Task OnGetAsync()
    {
        Locations = context.Location
            .Include(l => l.AvailableServices)
            .ToList();
        Statuses = context.Status.ToList();
        return Task.CompletedTask;
    }
    
    public IActionResult OnGetLocationsServices(int id) 
    {
        var location = context.Location
            .Include(l => l.AvailableServices)
            .FirstOrDefault(l => l.IdLocation == id);
        if (location == null) return NotFound();
            
        Console.WriteLine($"Location id: {location.LocationName}");

        return Partial("_ServicesListPartial", location);
    }

    public IActionResult OnGetCalendar(int IdLocation, int IdService) 
    {
        Console.WriteLine($"OnGetCalendar called with LocationId: {IdLocation}, ServiceId: {IdService}");
    
        var location = context.Location
            .Include(l => l.AvailableServices)
            .FirstOrDefault(l => l.IdLocation == IdLocation);
        
        var service = context.AvailableService
            .FirstOrDefault(s => s.IdService == IdService);
        
        Console.WriteLine($"Service id: {service.IdService}");
        Console.WriteLine($"Service id: {location.IdLocation}");
    
        if (location == null || service == null)
        {
            return BadRequest("Invalid location or service");
        }
    
        var today = DateTime.Today;
        
        var existingBookings = context.Calendar
            .Include(c => c.Location)
            .Include(c => c.Status!)
            .Where(c => c.IdLocation == IdLocation &&
                        (c.Status.StatusName == "Confirmed" || c.Status.StatusName == "Pending") &&
                        c.StartTime >= today)
            .ToList();
        
        
        return Partial("_CalendarPartial", (location, service, existingBookings));
    }
}