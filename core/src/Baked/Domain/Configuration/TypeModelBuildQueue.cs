using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelBuildQueue
{
    readonly ModelCollection<TypeModelReference>.KeyedCollection _data = [];

    public bool IsEmpty => _data.Count == 0;

    public void EnqueueAll(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            Enqueue(type);
        }
    }

    public TypeModelReference Enqueue(Type type)
    {
        if (_data.TryGetValue(TypeModelReference.IdFrom(type), out var result)) { return result; }

        _data.Add(result = new(type));

        return result;
    }

    public List<TypeModelReference> DequeueAll()
    {
        var result = new List<TypeModelReference>(_data);

        _data.Clear();

        return result;
    }
}