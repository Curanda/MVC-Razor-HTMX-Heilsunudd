using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please provide last name")]
    [MaxLength(30, ErrorMessage = "Last name can contain up to 30 characters")]
    [Display(Name = "Last name")]
    [Column(TypeName = "varchar(30)")]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "Please provide phone number")]
    [MaxLength(15, ErrorMessage = "Phone number can contain up to 15 digits")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [Column(TypeName = "varchar(15)")]
    [Display(Name = "Phone number")]
    public required string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Please provide email address")]
    [MaxLength(50, ErrorMessage = "Email address can contain up to 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "Email address")]
    public required string Email { get; set; }
    
    [MaxLength(8, ErrorMessage = "Kennitala can contain up to 8 digits")]
    [Column(TypeName = "int")]
    [Display(Name = "Kennitala")]
    public int Kennitala { get; set; }
    
    [Required(ErrorMessage = "Which service do you want to book?")]
    [Display(Name = "Service type")]
    public required string ServiceType { get; set; }
    

    [ForeignKey(nameof(ServiceType))]
    public AvailableService? Service { get; set; }
    
    [Required(ErrorMessage = "Please provide booking date")]
    [Display(Name = "Booking date")]
    [Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public required DateOnly BookingDate { get; set; }
    
    [Required(ErrorMessage = "Please provide booking time")]
    [Display(Name = "Booking time")]
    [Column(TypeName = "time")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public required TimeOnly BookingTime { get; set; }
    
    [Required(ErrorMessage = "Please provide location")]
    [Display(Name = "Service location")]
    public required string LocationName { get; set; }
    

    [ForeignKey(nameof(LocationName))]
    public Location? Location { get; set; }
    
    [Display(Name = "Booking status")]
    [Required(ErrorMessage = "Please provide booking status")]
    public required string BookingStatus { get; set; }
    

    [ForeignKey(nameof(BookingStatus))]
    public Status? Status { get; set; }
    
    [Display(Name = "Created date")]
    [Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}