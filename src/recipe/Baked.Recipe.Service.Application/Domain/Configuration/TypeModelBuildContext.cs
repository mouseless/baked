namespace Baked.Domain.Configuration;

public class TypeModelBuildContext
{
    readonly Type _type;
    readonly DomainModelBuilder _builder;

    internal TypeModelBuildContext(Type type, DomainModelBuilder builder)
    {
        _type = type;
        _builder = builder;
    }

    public Type Type => _type;

    public bool DomainTypesContain(Type type) =>
        _builder.DomainTypesContain(type);
}