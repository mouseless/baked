namespace Do.Domain.Model;

public class MethodModelCollection(IEnumerable<MethodModel> models)
    : ModelCollection<MethodModel>(models)
{
    public MethodModelCollection() : this([]) { }
    public ModelCollection<MethodModel> GetIndex<T>() where T : class
        => GetIndex(TypeModel.IdFrom(typeof(T)));
}
