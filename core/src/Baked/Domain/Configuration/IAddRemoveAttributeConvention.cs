namespace Baked.Domain.Configuration;

public interface IAddRemoveAttributeConvention : IDomainModelConvention
{
    bool AttributeRequiresIndex { get; }
}