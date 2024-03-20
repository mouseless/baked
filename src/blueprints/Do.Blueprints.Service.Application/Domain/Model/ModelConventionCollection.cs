namespace Do.Domain.Model;

public class ModelConventionCollection<T> : IEnumerable<AddAttributeConvention<T>> where T : IModelWithMetadata
{
    readonly List<AddAttributeConvention<T>> _conventions = [];
    List<AddAttributeConvention<T>>? _ordered = default!;

    public void Add(Attribute add, Func<T, bool> when, int order)
    {
        _conventions.Add(new(add, when, order));
    }

    internal void Apply(T model)
    {
        _ordered ??= [.. _conventions.OrderBy(p => p.Order)];

        foreach (var convention in _ordered)
        {
            if (convention.AppliesTo(model))
            {
                convention.Apply(model);
            }
        }
    }

    public IEnumerator<AddAttributeConvention<T>> GetEnumerator() =>
        ((IEnumerable<AddAttributeConvention<T>>)_conventions).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_conventions).GetEnumerator();
}