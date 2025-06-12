namespace Heilsunudd.Data.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class CheckBoxDisplayAttribute(string entityName, string selectableField, string relateIdParam): Attribute
{
    public string EntityName { get; set; } = entityName;
    public string SelectableField { get; set; } =  selectableField;
    public string RelateIdParam { get; set; } =  relateIdParam;
}