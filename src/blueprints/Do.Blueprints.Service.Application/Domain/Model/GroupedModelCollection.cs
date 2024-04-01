using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class GroupedModelCollection<T> : IEnumerable<T> where T : IModel
{
    readonly List<T> _models = [];
    readonly Dictionary<string, IEnumerable<T>> _groups = [];
    readonly Dictionary<Type, IEnumerable<T>> _index = [];

    public GroupedModelCollection(Dictionary<string, IEnumerable<T>> groups)
    {
        _groups = groups;

        foreach (var group in groups)
        {
            _models.AddRange(group.Value);
        }
    }

    public bool ContainsGroup(string name) =>
        _groups.ContainsKey(name);

    public bool TryGetGroup(string name, [NotNullWhen(true)] out IEnumerable<T>? methods) =>
       _groups.TryGetValue(name, out methods);

    internal void AddIndex(Type index) =>
        _index[index] = _models.Where(m => m is ICustomAttributesModel member && member.CustomAttributes.ContainsKey(index));

    public IEnumerable<T> Having<TAttribute>() where TAttribute : Attribute =>
       Having(typeof(TAttribute));

    public IEnumerable<T> Having(Type attributeType) =>
        _index.TryGetValue(attributeType, out var result) ? result : [];

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
