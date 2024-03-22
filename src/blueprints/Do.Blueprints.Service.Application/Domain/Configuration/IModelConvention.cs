using Do.Domain.Configuration;
using Do.Domain.Model;

namespace Do.Domain.Convention;

public interface IModelConvention<T>
    where T : IModel
{
    int Order { get; }
    bool AppliesTo(T model);
    void Apply(T model);

    IModelConvention<T> Initialize(ModelConfigurators configurators);
}
