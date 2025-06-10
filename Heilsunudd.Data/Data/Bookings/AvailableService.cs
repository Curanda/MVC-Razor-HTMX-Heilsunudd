using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Data.Data.Bookings;

[Index(nameof(ServiceName), IsUnique = true)]
public class AvailableService
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Editable(false)]
    public int IdService { get; set; }
    
    [Required(ErrorMessage = "Please provide service type")]
    [MaxLength(30, ErrorMessage = "Service type can contain up to 30 characters")]
    [Display(Name = "Service name")]
    public required string ServiceName { get; set; }
    
    [Required(ErrorMessage = "Please provide service duration")]
    [Range(10, 120, ErrorMessage = "Service duration must be between 10 and 120 minutes")]
    [Display(Name = "Service duration")]
    public required int ServiceDuration { get; set; }
    
    [Required(ErrorMessage = "Please provide service price")]
    [Range(2000.00, 50000.00, ErrorMessage = "Service price must be between 2000 and 50000")]
    [Display(Name = "Service price")]
    public required decimal ServicePrice { get; set; }
    
    [Required(ErrorMessage = "Please provide service description")]
    [MaxLength(1200, ErrorMessage = "Service description can contain up to 1200 characters")]
    [Display(Name = "Service description")]
    public required string ServiceDescription { get; set; }
    
    [Required(ErrorMessage = "Please provide service image URL")]
    [MaxLength(200, ErrorMessage = "Service image URL can contain up to 200 characters")]
    [Display(Name = "Service image URL")]
    [Url(ErrorMessage = "Invalid URL format")]
    public required string ServiceImageUrl { get; set; }
    
    [Required(ErrorMessage = "Please specify if the service is active")]
    [Display(Name = "Is active")]
    public required bool ServiceIsActive { get; set; }
    
    [Display(Name = "Available at locations")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}


