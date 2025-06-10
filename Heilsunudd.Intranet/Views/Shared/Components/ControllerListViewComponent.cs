using Microsoft.AspNetCore.Mvc;
using static Heilsunudd.Intranet.Controllers.MetadataController;

namespace Heilsunudd.Intranet.Views.Shared.Components;

public class ControllerListViewComponent()
    : ViewComponent
{

    public IViewComponentResult Invoke()
    {
        var controllers = GetModelNames().ToArray();

        return View(controllers);
    }
}
