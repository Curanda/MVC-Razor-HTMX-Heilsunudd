using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Heilsunudd.Data.Data.Bookings;

public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    public int IdBooking { get; set; }
    
    [Required(ErrorMessage = "Please provide first name")]
    [MaxLength(30, ErrorMessage = "First name can contain up to 30 characters")]
    [Display(Name = "First name")]
    [Column(TypeName = "varchar(30)")]
    [DataType(DataType.Text)]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please provide last name")]
    [MaxLength(30, ErrorMessage = "Last name can contain up to 30 characters")]
    [Display(Name = "Last name")]
    [Column(TypeName = "varchar(30)")]
    [DataType(DataType.Text)]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "Please provide phone number")]
    [MaxLength(15, ErrorMessage = "Phone number can contain up to 15 digits")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [Column(TypeName = "int")]
    [Display(Name = "Phone number")]
    [DataType(DataType.PhoneNumber)]
    public required PhoneAttribute PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Please provide email address")]
    [MaxLength(50, ErrorMessage = "Email address can contain up to 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "Email address")]
    [DataType(DataType.EmailAddress)]
    public required EmailAddressAttribute Email { get; set; }
    
    [MaxLength(8, ErrorMessage = "Kennitala can contain up to 8 digits")]
    [Column(TypeName = "int")]
    [Display(Name = "Kennitala")]
    public int Kennitala { get; set; }
    
    [Required(ErrorMessage = "Which service do you want to book?")]
    [Display(Name = "Service type")]
    [ForeignKey("AvailableService.ServiceType")]
    public string ServiceType { get; set; }
    
    [Required(ErrorMessage = "Please provide booking date")]
    [Display(Name = "Booking date")]
    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public required DateOnly BookingDate { get; set; }
    
    [Required(ErrorMessage = "Please provide booking time")]
    [Display(Name = "Booking time")]
    [DataType(DataType.Time)]
    [Column(TypeName = "time")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public required TimeOnly BookingTime { get; set; }
    
    [Required(ErrorMessage = "Please provide location")]
    [Display(Name = "Service location")]
    [ForeignKey("Location.LocationName")]
    public required string LocationName { get; set; }
    
    [ForeignKey("Status.StatusName")]
    [Display(Name = "Booking status")]
    [Required(ErrorMessage = "Please provide booking status")]
    public required string BookingStatus { get; set; }
}