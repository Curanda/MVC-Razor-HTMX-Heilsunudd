using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.Bookings;

public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "int")]
    [Editable(false)]
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
    
    [MaxLength(10, ErrorMessage = "Kennitala can contain up to 10 digits")]
    [Column(TypeName = "varchar(10)")]
    [Display(Name = "Kennitala")]
    public string? Kennitala { get; set; } 
    

    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int IdService { get; set; }
    
    [ForeignKey(nameof(IdService))]
    public AvailableService? AvailableService { get; set; }
    
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
    

    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int IdLocation { get; set; }
    
    [ForeignKey(nameof(IdLocation))]
    public Location? Location { get; set; }
    

    [ScaffoldColumn(false)]
    [Browsable(false)]
    public required int IdStatus { get; set; }
    
    [ForeignKey(nameof(IdStatus))]
    public Status? Status { get; set; }
    
    [Display(Name = "Created date")]
    [Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}