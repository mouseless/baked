using Do.Domain.Convention;
using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class DomainConventions
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];
    public ModelConventionCollection<ParameterModel> Parameter { get; } = [];
    public ModelConventionCollection<PropertyModel> Property { get; } = [];
}
