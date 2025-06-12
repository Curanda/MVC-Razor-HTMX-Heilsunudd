using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Heilsunudd.Data.CustomAttributes;
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
    
    [ScaffoldColumn(false)]
    [Browsable(false)]
    [Display(Name = "Booking Id")]
    [RelatedEntity(typeof(Booking))]
    public required int IdBooking { get; set; }
    
    [ForeignKey(nameof(IdBooking))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [SelectDisplay("Booking","IdBooking", "IdBooking")]
    public Booking? Booking { get; set; }
    
    [ScaffoldColumn(false)]
    [Browsable(false)]

    [Display(Name = "Location Id")]
    [RelatedEntity(typeof(Location))]
    public required int IdLocation { get; set; }
    
    [ForeignKey(nameof(IdLocation))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [SelectDisplay("Location","LocationName", "IdLocation")]
    public Location? Location { get; set; }
    
    
    [ScaffoldColumn(false)]
    [Browsable(false)]
    [Display(Name = "Status Id")]
    [RelatedEntity(typeof(Status))]
    public required int IdStatus { get; set; }
    
    [ForeignKey(nameof(IdStatus))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [SelectDisplay("Status","StatusName", "IdStatus")]
    public Status? Status { get; set; }
}