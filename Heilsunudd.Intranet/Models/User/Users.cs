using Microsoft.AspNetCore.Identity;

namespace Heilsunudd.Intranet.Models.User;

public class Users : IdentityUser
{
    public string FullName { get; private set; }
}