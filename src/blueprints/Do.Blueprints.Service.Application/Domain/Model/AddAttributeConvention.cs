namespace Do.Domain.Model;

public class AddAttributeConvention<T>(Attribute _attribute, Func<T, bool> _when, int _order)
    where T : IModelWithMetadata
{
    public int Order => _order;
    public bool AppliesTo(T model) => _when(model);
    public void Apply(T model) => model.CustomAttributes.TryAdd(new(_attribute.GetType()));
}
