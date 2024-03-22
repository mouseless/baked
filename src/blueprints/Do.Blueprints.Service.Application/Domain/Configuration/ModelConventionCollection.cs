using Do.Domain.Configuration;
using Do.Domain.Model;

namespace Do.Domain.Convention;

public class ModelConventionCollection<T>() : IEnumerable<IModelConvention<T>>
    where T : IModelWithMetadata
{
    List<IModelConvention<T>> _conventions = [];

    public void Add(IModelConvention<T> convention) => _conventions.Add(convention);

    internal ModelConventionCollection<T> Initialize(ModelConfigurators configurators)
    {
        foreach (var item in _conventions)
        {
            item.Initialize(configurators);
        }

        _conventions = [.. _conventions.OrderBy(c => c.Order)];

        return this;
    }

    internal void Apply(ModelCollection<T> collection)
    {
        foreach (var convention in _conventions)
        {
            foreach (var model in collection)
            {
                if (convention.AppliesTo(model))
                {
                    convention.Apply(model);
                }
            }
        }
    }

    public IEnumerator<IModelConvention<T>> GetEnumerator() =>
        ((IEnumerable<IModelConvention<T>>)_conventions).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_conventions).GetEnumerator();
}