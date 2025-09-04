namespace Baked.Domain.Model;

public interface IMutableAttributeCollection
{
    void Add(Attribute attribute);
    void Remove(Type type);

    public void Remove<T>() where T : Attribute =>
        Remove(typeof(T));
}