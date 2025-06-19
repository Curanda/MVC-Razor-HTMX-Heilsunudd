using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.CMS;

public class CustomMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Editable(false)]
    public int IdCustomMessage { get; set; }
    
    [Required(ErrorMessage = "Please provide Name")]
    [MaxLength(30, ErrorMessage = "Name can contain up to 30 characters")]
    [Display(Name = "Name")]
    [Column(TypeName = "varchar(30)")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Please provide email address")]
    [MaxLength(50, ErrorMessage = "Email address can contain up to 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "Email address")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Please provide message")]
    [MaxLength(2000, ErrorMessage = "Message can contain up to 2000 characters")]
    [Display(Name = "Message")]
    public required string Message { get; set; }
}