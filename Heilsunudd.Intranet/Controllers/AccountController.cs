using Microsoft.AspNetCore.Mvc;

namespace Heilsunudd.Intranet.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
}