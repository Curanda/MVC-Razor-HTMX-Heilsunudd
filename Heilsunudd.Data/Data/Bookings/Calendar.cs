using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Data.Data.Bookings;

[Index(nameof(LocationName), nameof(StartTime), IsUnique = true)]
public class Calendar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCalendar { get; set; }
    
    [DataType(DataType.DateTime)]
    [Column(TypeName = "datetime")]
    [Display(Name = "Start Time")]
    public required DateTime StartTime { get; set; }
    
    [DataType(DataType.Time)]
    [Column(TypeName = "time")]
    [Display(Name = "End Time")]
    public required TimeOnly EndTime { get; set; }
    
    [Display(Name = "Booking ID")]
    [ForeignKey("Booking.IdBooking")]
    [Column(TypeName = "int")]
    public required int IdBooking { get; set; }
    
    [Display(Name = "Location Name")]
    [ForeignKey("Location.LocationName")]
    public required string LocationName { get; set; }
 
    [ForeignKey("Status.StatusName")]
    [Display(Name = "Status")]
    public required string StatusName { get; set; }
}