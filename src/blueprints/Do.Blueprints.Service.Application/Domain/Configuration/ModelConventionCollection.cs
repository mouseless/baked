using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConventionCollection<TModel>() : List<IModelConvention<TModel>>
    where TModel : IModel
{
    public void Add(ModelConvention<TModel, AttributeAdder> convention)
    {
        Add((IModelConvention<TModel>)convention);
    }
}