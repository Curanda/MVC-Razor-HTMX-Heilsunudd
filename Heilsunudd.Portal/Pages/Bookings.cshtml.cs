using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heilsunudd.Data.Data.Bookings;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Calendar = Heilsunudd.Data.Data.Bookings.Calendar;


namespace Heilsunudd.Pages;

public class BookingsModel(ILogger<BookingsModel> logger, HeilsunuddDbContext context) : PageModel
{
    private readonly ILogger<BookingsModel> _logger = logger;
    public IList<Location> Locations { get;set; } = new List<Location>();
    public IList<Service> Services { get;set; } = new List<Service>();
    public IList<Status> Statuses { get;set; } = new List<Status>();
    public IList<Calendar> ExistingCalendarEntries { get;set; } = new List<Calendar>();


    public async Task OnGetAsync()
    {
        Locations = await context.Location
            .Include(l => l.Services)
            .ToListAsync();
        Statuses = await context.Status.ToListAsync();
    }
    
    public IActionResult OnGetLocationsServices(int id) 
    {
        var location = context.Location
            .Include(l => l.Services)
            .FirstOrDefault(l => l.IdLocation == id);
        if (location == null) return NotFound();
            
        Console.WriteLine($"Location id: {location.LocationName}");

        return Partial("_ServicesListPartial", location);
    }

    public IActionResult OnGetCalendar(int idLocation, int idService) 
    {
        Console.WriteLine($"OnGetCalendar called with LocationId: {idLocation}, ServiceId: {idService}");
    
        var location = context.Location
            .Include(l => l.Services)
            .FirstOrDefault(l => l.IdLocation == idLocation);
        
        var service = context.Service
            .FirstOrDefault(s => s.IdService == idService);

        if (service != null && location != null)
        {
            Console.WriteLine($"Service id: {service.IdService}");
            Console.WriteLine($"Location id: {location.IdLocation}");

            var today = DateTime.Today;

            var existingBookings = context.Calendar
                .Include(c => c.Location)
                .Include(c => c.Status!)
                .Where(c => c.IdLocation == idLocation &&
                            c.Status != null &&
                            (c.Status.StatusName == "Confirmed" || c.Status.StatusName == "Pending") &&
                            c.StartTime >= today)
                .ToList();


            return Partial("_CalendarPartial", (location, service, existingBookings));
        }
        
        return NotFound();
    }
    
    public IActionResult OnGetTimeslots(int idLocation, string selectedDate)
    {
        Console.WriteLine($"{selectedDate}");
        
        var allSlots = new List<string>()
        {
            "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
        };
        
        if (!DateOnly.TryParseExact(selectedDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var targetDate))
        {
            Console.WriteLine($"Failed to parse date: {selectedDate}");
            return BadRequest($"Invalid date format: {selectedDate}. Expected dd.MM.yyyy");
        }
        
        var existingBookings = context.Booking.Include(b=>b.Location)
            .Include(b=>b.Service)
            .Include(b=>b.Status)
            .Where(b=>b.IdLocation == idLocation && b.Status != null && (b.Status.StatusName == "Confirmed" || b.Status.StatusName == "Pending") && b.BookingDate == targetDate)
            .ToList();

        if (existingBookings.IsNullOrEmpty())
        {
            Console.WriteLine("No existing bookings found.");
            ViewData["selectedDate"] = selectedDate;
            return Partial("_TimeSlotPartial", allSlots);
        };
    
        
        var bookedSlots = existingBookings.Select(b => b.BookingTime.ToString("HH:mm")).ToList();
        
        var freeSlots = allSlots.Except(bookedSlots).ToList();
        
        Console.WriteLine($"{allSlots.Count} slots, {bookedSlots.Count} booked, {freeSlots.Count} free. First slot {freeSlots.First()}. Existing bookings {existingBookings.Count}. Existing booking start time: {existingBookings.First().BookingTime}.");
    

        ViewData["selectedDate"] = selectedDate;
        return Partial("_TimeSlotPartial", freeSlots);
    }

    public IActionResult OnGetBookingModal(int idLocation, int idService, string selectedDate, string selectedTimeslot)
    {
        Console.WriteLine($"OnGetBookingModal called with LocationId: {idLocation}, ServiceId: {idService}, Date: {selectedDate}, Timeslot: {selectedTimeslot}");
        
        var timeSlot = TimeOnly.ParseExact(selectedTimeslot, "HH:mm", CultureInfo.InvariantCulture);
        var bookingDate = DateOnly.ParseExact(selectedDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        
        var status = context.Status.FirstOrDefault(s => s.StatusName == "Pending")!;
        var location = context.Location.FirstOrDefault(l => l.IdLocation == idLocation)!;
        var service = context.Service.FirstOrDefault(s => s.IdService == idService)!;
        
        var newBooking = new Booking()
        {
            FirstName = "",
            LastName = "",
            Kennitala = "",
            Email = "",
            PhoneNumber = "",
            BookingDate = bookingDate,
            BookingTime = timeSlot,
            IdLocation = idLocation,
            Location = location,
            IdService = idService,
            Service = service,
            IdStatus = status.IdStatus,
            Status = status,
            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
        };
        return Partial("_BookingModal", newBooking);
    }

    public async Task<IActionResult> OnPostBookingForm(Booking booking)
    {
        Console.WriteLine($"OnPostBookingForm called with LocationId: {booking.IdLocation}, ServiceId: {booking.IdService}, Date: {booking.BookingDate}, Timeslot: {booking.BookingTime}");
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is not valid.");
            return Partial("_BookingModal", booking);
        }
        
        context.Booking.Add(booking);
        await context.SaveChangesAsync();
        return Partial("_BookingSuccessfulModal", booking);
    }

    
}