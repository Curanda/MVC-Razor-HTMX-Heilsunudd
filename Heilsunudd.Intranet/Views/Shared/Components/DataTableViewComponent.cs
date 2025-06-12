
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Heilsunudd.Intranet.Views.Shared.Components;
public class DataTableViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<object>? data, string title = "Data")
    {
        if (data == null || !data.Any())
        {
            return View("Empty");
        }

        var model = new DataTableViewModel
        {
            Title = title,
            Data = data,
            Properties = GetProperties(data.First().GetType())
        };

        return View(model);
    }

    private List<PropertyDisplayInfo> GetProperties(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead &&
                        (p.PropertyType.IsValueType || 
                         p.PropertyType == typeof(string) ||
                         Nullable.GetUnderlyingType(p.PropertyType) != null))
            .Select(p => new PropertyDisplayInfo
            {
                Property = p,
                DisplayName = p.GetCustomAttribute<DisplayAttribute>()?.Name ?? 
                              SplitCamelCase(p.Name),
                FormatString = p.GetCustomAttribute<DisplayFormatAttribute>()?.DataFormatString
            })
            .ToList();
    }

    private string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
    }
    
    
}

public class DataTableViewModel
{
    public string Title { get; set; }
    public IEnumerable<object> Data { get; set; }
    public List<PropertyDisplayInfo> Properties { get; set; }
}

public class PropertyDisplayInfo
{
    public PropertyInfo Property { get; set; }
    public string DisplayName { get; set; }
    public string? FormatString { get; set; }
}