using Heilsunudd.Intranet.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Intranet.Data;

public class UserContext : IdentityDbContext<Users>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
}