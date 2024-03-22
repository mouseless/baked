namespace Do.Domain.Model;

public class MethodModelCollection(IEnumerable<MethodModel> models)
    : ModelCollection<MethodModel>(models)
{
    public MethodModelCollection() : this([]) { }

    public ModelCollection<MethodModel> WithAttribute<T>() where T : Attribute
        => GetIndex(TypeModel.IdFrom(typeof(T)));
}
