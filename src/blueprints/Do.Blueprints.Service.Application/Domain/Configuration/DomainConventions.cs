using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class DomainConventions
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];
    public ModelConventionCollection<ParameterModel> Parameter { get; } = [];
    public ModelConventionCollection<PropertyModel> Property { get; } = [];

    internal DomainConventions Initialize(BuildDomainContext buildDomainContext)
    {
        Type.Initialize(buildDomainContext);
        Method.Initialize(buildDomainContext);
        Parameter.Initialize(buildDomainContext);
        Property.Initialize(buildDomainContext);

        return this;
    }
}
