using System.ComponentModel.DataAnnotations;

namespace Heilsunudd.Intranet.ViewModels;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
    [DataType(DataType.Password)]
    [Compare("ConfirmNewPassword", ErrorMessage = "Passwords do not match")]
    [Display(Name = "Set new password")]
    public string NewPassword { get; set; }
    
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    public string ConfirmNewPassword { get; set; }
}