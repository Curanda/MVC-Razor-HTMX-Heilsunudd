using Heilsunudd.Data.Data.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Heilsunudd.Intranet.Views.Shared.Components;

public class StatusViewComponent: ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<Status> statuses)
    {
        return View(statuses);
    }

}