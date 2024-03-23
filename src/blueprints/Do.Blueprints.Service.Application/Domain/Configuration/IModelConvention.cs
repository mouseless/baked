using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IModelConvention<T>
    where T : IModel
{
    int Order { get; }
    bool AppliesTo(T model);
    void Apply(T model);
    void Initialize(BuildDomainContext domainBuilderContext);
}
