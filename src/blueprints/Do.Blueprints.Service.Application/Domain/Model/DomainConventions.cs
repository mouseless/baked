namespace Do.Domain.Model;

public class DomainConventions
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];
}
