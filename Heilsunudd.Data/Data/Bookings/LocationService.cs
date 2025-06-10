using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.Bookings;

public class LocationService
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdService { get; set; }
    
    public int IdLocation { get; set; }
    public Location? Location { get; set; }

    public int IdAvailableService { get; set; }
    public AvailableService? AvailableService { get; set; }
}