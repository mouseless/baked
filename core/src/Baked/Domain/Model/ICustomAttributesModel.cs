namespace Baked.Domain.Model;

public interface ICustomAttributesModel
{
    AttributeTargets Target { get; }
    AttributeCollection CustomAttributes { get; }
}