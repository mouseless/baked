namespace Do.Domain.Model;

public interface IModelConvention<T>
    where T : IModel
{
    int Order { get; }
    bool AppliesTo(T model);
    void Apply(T model);
}
