namespace Heilsunudd.Data.CustomAttributes;

[AttributeUsage(AttributeTargets.All)]
public class RelatedEntityAttribute(object relatedEntity) : Attribute
{
    public object Entity { get; set; } = relatedEntity;
}