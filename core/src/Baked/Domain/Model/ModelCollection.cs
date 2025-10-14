using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    readonly KeyedCollection _models = [];
    readonly Dictionary<Type, IEnumerable<T>> _index = [];

    public ModelCollection(IEnumerable<T> models)
        : this()
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    public T this[string id] =>
        _models[id];

    public T this[Type type] =>
        _models[TypeModelReference.IdFrom(type)];

    public int Count => _models.Count;

    internal void AddIndex(Type index) =>
        _index[index] = this.Where(m => m is ICustomAttributesModel member && member.Has(index));

    public bool ContainsModel(T? model) =>
        _models.Contains(model?.Id ?? string.Empty);

    public bool Contains(string id) =>
        _models.Contains(id);

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
       _models.TryGetValue(id, out model);

    public IEnumerable<T> Having<TAttribute>() where TAttribute : Attribute =>
        Having(typeof(TAttribute));

    public IEnumerable<T> Having(Type attributeType) =>
        _index.TryGetValue(attributeType, out var result) ? result : [];

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class KeyedCollection : KeyedCollection<string, T>
    {
        protected override string GetKeyForItem(T item) => item.Id;
    }
}