using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Heilsunudd.Intranet.Views.Shared.Components;

public class ControllerListViewComponent : ViewComponent
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

    public ControllerListViewComponent(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public IViewComponentResult Invoke()
    {
        var controllers = _actionDescriptorCollectionProvider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(x => x.ControllerName)
            .Distinct()
            .Select(name => new ControllerInfo
            {
                OriginalName = name,
                DisplayName = FormatControllerName(name)
            })
            .OrderBy(x => x.DisplayName)
            .ToArray();

        return View(controllers);
    }
    
    private string FormatControllerName(string controllerName)
    {
        if (controllerName.EndsWith("Controller"))
        {
            controllerName = controllerName.Substring(0, controllerName.Length - 10);
        }
        
        return Regex.Replace(controllerName, "([a-z])([A-Z])", "$1 $2");
    }
}

public class ControllerInfo
{
    public string OriginalName { get; set; }
    public string DisplayName { get; set; }
}