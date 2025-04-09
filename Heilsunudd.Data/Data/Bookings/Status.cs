using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.Bookings;

public class Status
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdStatus { get; set; }

    [Required(ErrorMessage = "Please provide status name")]
    [MaxLength(10, ErrorMessage = "Status name can contain up to 10 characters")]
    [Display(Name = "Status name")]
    public required string StatusName { get; set; }
}