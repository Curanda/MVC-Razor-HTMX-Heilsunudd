using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Data.Data.Bookings;
public class Calendar
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Editable(false)]
    public int IdCalendar { get; set; }
                                                                              
    [DataType(DataType.DateTime)]
    [Column(TypeName = "datetime")]
    [Display(Name = "Start Time")]
    public required DateTime StartTime { get; set; }
    
    [DataType(DataType.DateTime)]
    [Column(TypeName = "datetime")]
    [Display(Name = "End Time")]
    public required DateTime EndTime { get; set; }
    
    // [Display(Name = "Booking ID")]
    // [Column(TypeName = "int")]
    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int IdBooking { get; set; }
    
    [ForeignKey(nameof(IdBooking))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Booking? Booking { get; set; }
    
    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int IdLocation { get; set; }
    
    [ForeignKey(nameof(IdLocation))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Location? Location { get; set; }
    
    
    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int StatusId { get; set; }
    
    [ForeignKey(nameof(StatusId))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Status? Status { get; set; }
}