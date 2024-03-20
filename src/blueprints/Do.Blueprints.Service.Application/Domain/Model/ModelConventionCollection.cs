namespace Do.Domain.Model;

public class ModelConventionCollection<T> : IEnumerable<IModelConvention<T>> where T : IModelWithMetadata
{
    readonly List<IModelConvention<T>> _conventions = [];
    List<IModelConvention<T>>? _ordered = default!;

    public void Add(Attribute add, Func<T, bool> when, int order)
    {
        _conventions.Add(new AddAttributeConvention<T>(add, when, order));
    }

    internal void Apply(T model, ModelCache<T> cache)
    {
        _ordered ??= [.. _conventions.OrderBy(p => p.Order)];

        foreach (var convention in _ordered)
        {
            if (convention.AppliesTo(model))
            {
                convention.Apply(model, cache);
            }
        }
    }

    public IEnumerator<IModelConvention<T>> GetEnumerator() =>
        ((IEnumerable<IModelConvention<T>>)_conventions).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_conventions).GetEnumerator();
}