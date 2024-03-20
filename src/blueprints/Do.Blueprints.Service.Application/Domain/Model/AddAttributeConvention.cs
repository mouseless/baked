namespace Do.Domain.Model;

public class AddAttributeConvention<T>(Attribute _attribute, Func<T, bool> _when, int _order) : IModelConvention<T>
    where T : IModelWithMetadata
{
    public int Order => _order;
    public bool AppliesTo(T model) => _when(model);
    public void Apply(T model, ModelCache<T> cache)
    {
        var id = TypeModel.IdFrom(_attribute.GetType());
        var typeModel = new TypeModel(_attribute.GetType(), id);

        if (model.CustomAttributes.TryAdd(typeModel))
        {
            if (!cache.TryGetValue(id, out var collection))
            {
                cache[id] = [];
            }

            cache[id].TryAdd(model);
        }
    }
}
