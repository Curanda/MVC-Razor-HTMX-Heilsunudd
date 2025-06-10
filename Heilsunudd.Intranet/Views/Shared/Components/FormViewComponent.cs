namespace Heilsunudd.Intranet.Views.Shared.Components;

using Microsoft.AspNetCore.Mvc;

[ViewComponent(Name = "Form")]
public class FormViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(object model, string title = "Form", string submitText = "Save", string cancelUrl = "/")
    {
        if (model == null)
        {
            return Content("Error: No model provided");
        }

        var viewModel = new FormViewModel
        {
            Title = title,
            SubmitText = submitText,
            CancelUrl = cancelUrl,
            Model = model,
            ModelTypeName = model.GetType().Name
        };

        return View(viewModel);
    }
}

public class FormViewModel
{
    public string Title { get; set; }
    public string SubmitText { get; set; }
    public string CancelUrl { get; set; }
    public object Model { get; set; }
    public string ModelTypeName { get; set; }
}