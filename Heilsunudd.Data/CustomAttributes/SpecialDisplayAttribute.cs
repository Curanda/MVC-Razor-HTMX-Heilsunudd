namespace Heilsunudd.Data.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class SpecialDisplayAtttribute(string entityName, string selectableField, string relateIdParam, string typeOfDisplay): Attribute
{
    public string EntityName { get; set; } = entityName;
    public string SelectableField { get; set; } =  selectableField;
    public string RelateIdParam { get; set; } =  relateIdParam;
    public string TypeOfDisplay { get; set; } = typeOfDisplay;
}