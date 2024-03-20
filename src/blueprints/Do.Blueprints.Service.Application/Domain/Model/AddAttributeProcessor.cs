namespace Do.Domain.Model;

public class AddAttributeProcessor(Attribute _attribute, Func<TypeModel, bool> _when, int _order)
{
    public int Order => _order;
    public bool AppliesTo(TypeModel typeModel) => _when(typeModel);
    public void Apply(TypeModel typeModel) => typeModel.CustomAttributes.TryAdd(new(_attribute.GetType()));
}
