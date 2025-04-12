using System.ComponentModel.DataAnnotations;

namespace Heilsunudd.Intranet.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is requiered")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; private set; }
    
    [Required(ErrorMessage = "Password is requiered")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; private set; }
    
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}