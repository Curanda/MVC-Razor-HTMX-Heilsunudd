using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.CMS;

public class ContactInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdContactInformation { get; set; }
    
    // phone
    [Required(ErrorMessage = "Please provide phone number")]
    [MaxLength(20, ErrorMessage = "Phone number can contain up to 20 characters")]
    [Display(Name = "Phone number")]
    [DataType(DataType.PhoneNumber)]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [Column(TypeName = "nvarchar(20)")]
    public required PhoneAttribute PhoneNumber { get; set; }
    
    //kennitala
    [Required(ErrorMessage = "Please provide kennitala")]
    [MaxLength(8, ErrorMessage = "Kennitala can contain up to 8 characters")]
    [Display(Name = "Kennitala")]
    [Column(TypeName = "int")]
    public required int Kennitala { get; set; }
    
    // company name
    [Required(ErrorMessage = "Please provide company name")]
    [MaxLength(50, ErrorMessage = "Company name can contain up to 50 characters")]
    [Display(Name = "Company name")]
    [Column(TypeName = "nvarchar(50)")]
    public required string CompanyName { get; set; }
}