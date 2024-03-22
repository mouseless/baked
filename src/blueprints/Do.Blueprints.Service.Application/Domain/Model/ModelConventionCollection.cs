namespace Do.Domain.Model;

public class ModelConventionCollection<T>() : IEnumerable<IModelConvention<T>>
    where T : IModelWithMetadata
{
    readonly List<IModelConvention<T>> _conventions = [];

    public ModelConventionCollection<T> Add(Attribute add, Func<T, bool> when, int order)
    {
        _conventions.Add(new AddAttributeConvention<T>(add, when, order));

        return this;
    }

    internal void Apply(ModelCollection<T> collection)
    {
        foreach (var convention in _conventions.OrderBy(c => c.Order))
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