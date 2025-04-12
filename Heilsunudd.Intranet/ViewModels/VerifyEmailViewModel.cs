using System.ComponentModel.DataAnnotations;

namespace Heilsunudd.Intranet.ViewModels;

public class VerifyEmailViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}