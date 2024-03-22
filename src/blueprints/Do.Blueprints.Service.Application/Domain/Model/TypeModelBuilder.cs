namespace Do.Domain.Model;

internal class TypeModelBuilder(TypeModel _typeModel, ModelIndex<TypeModel> _index)
{
    public void AddCustomAttribute(TypeModel attribute)
    {
        _typeModel.CustomAttributes.TryAdd(attribute);

        _index[((IModel)attribute).Id].TryAdd(_typeModel);
    }
}