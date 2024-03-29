using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class ModelConventionProcessor<T>(ModelConventionCollection<T> _conventions) : IModelCollectionConfigurer<T>
    where T : IModel
{
    void Apply(ModelCollection<T> models)
    {
        foreach (var convention in _conventions)
        {
            foreach (var model in models)
            {
                convention.Apply(model);
            }
        }
    }

    void IModelCollectionConfigurer<T>.Apply(ModelCollection<T> models) => Apply(models);
}