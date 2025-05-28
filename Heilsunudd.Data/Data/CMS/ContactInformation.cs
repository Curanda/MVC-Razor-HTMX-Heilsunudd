using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Data.Data.CMS;

[Index(nameof(Kennitala), IsUnique = true)]
public class ContactInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Editable(false)]
    public int IdContactInformation { get; set; }
        
    [Required(ErrorMessage = "Please provide phone number")]
    [MaxLength(20, ErrorMessage = "Phone number can contain up to 20 characters")]
    [Display(Name = "Phone number")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [Column(TypeName = "nvarchar(20)")]
    public required string PhoneNumber { get; set; }
        
    [Required(ErrorMessage = "Please provide kennitala")]
    [MaxLength(10, ErrorMessage = "Kennitala can contain up to 10 characters")]
    [Display(Name = "Kennitala")]
    [Column(TypeName = "nvarchar(10)")]
    [RegularExpression(@"^\d{6}-?\d{4}$", ErrorMessage = "Invalid kennitala format")]
    public required string Kennitala { get; set; }
        
    [Required(ErrorMessage = "Please provide company name")]
    [MaxLength(50, ErrorMessage = "Company name can contain up to 50 characters")]
    [Display(Name = "Company name")]
    [Column(TypeName = "nvarchar(50)")]
    public required string CompanyName { get; set; }
}