using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.Bookings;

public class Location
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdLocation { get; set; }
    
    [Required(ErrorMessage = "Location name is required")]
    [MaxLength(30, ErrorMessage = "Location name can contain up to 30 characters")]
    [Column(TypeName = "varchar(30)")]
    [Display(Name = "Location name")]
    public required string LocationName { get; set; }
    
    [Required(ErrorMessage = "Town is required")]
    [MaxLength(30, ErrorMessage = "Town name can contain up to 30 characters")]
    [Column(TypeName = "varchar(30)")]
    [Display(Name = "Town name")]
    public required string LocationTown { get; set; }
    
    [Required(ErrorMessage = "Provide the street on which your venue is located")]
    [MaxLength(30, ErrorMessage = "Street name on which your venue is located can't be longer than 30 characters")]
    [Column(TypeName = "varchar(30)")]
    [Display(Name = "Street name")]
    public required string LocationStreet { get; set; }
    
    [Required(ErrorMessage = "Provide the number of the building on which your venue is located")]
    [MaxLength(10, ErrorMessage = "Street number can't be longer than 10 characters")]
    [Column(TypeName = "varchar(10)")]
    [Display(Name = "House number")]
    public required string LocationHouseNumber { get; set; }
    
    [MaxLength(200, ErrorMessage = "Additional information about location cannot be longer than 200 characters")]
    [Display(Name = "Additional information on how to get to the venue")]
    [Column(TypeName = "nvarchar(200)")]
    public string? LocationAdditionalInfo { get; set; }
    
    [Required(ErrorMessage = "You must provide GPS coordinates for your venue")]
    [MaxLength(50, ErrorMessage = "GPS coordinates can't be longer than 50 characters")]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "GPS coordinates")]
    public required string LocationCoordinates { get; set; }
    
    [Display(Name = "Location description")]
    [Column(TypeName = "nvarchar(500)")]
    [MaxLength(500, ErrorMessage = "Location description can't be longer than 500 characters")]
    [Required(ErrorMessage = "Location description is required")]
    public required string LocationDescription { get; set; }
    
    [Required(ErrorMessage = "Location image URL is required")]
    [MaxLength(200, ErrorMessage = "Location image URL can't be longer than 200 characters")]
    [Column(TypeName = "varchar(200)")]
    [Display(Name = "Location image URL")]
    [Url(ErrorMessage = "Invalid URL format")]
    public required string LocationImageUrl { get; set; }
    
    [Display(Name = "Activate location?")]
    [Required(ErrorMessage = "You must specify if the location is active")]
    [Column(TypeName = "bit")]
    public required bool LocationIsActive { get; set; }
    
}