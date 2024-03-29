using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IModelConvention<TModel> where TModel : IModel
{
    int Order { get; }
    void Apply(TModel model);
}
