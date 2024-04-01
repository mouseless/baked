using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class MethodModelCollection : IEnumerable<MethodModel>
{
    readonly List<MethodModel> _models = [];
    readonly Dictionary<string, IEnumerable<MethodModel>> _groups = [];
    readonly Dictionary<Type, IEnumerable<MethodModel>> _index = [];

    public MethodModelCollection(Dictionary<string, IEnumerable<MethodModel>> groups)
    {
        _groups = groups;

        foreach (var group in groups)
        {
            _models.AddRange(group.Value);
        }
    }

    public int Count => _models.Count;

    public bool Contains(string name) =>
        _groups.ContainsKey(name);

    public bool TryGetGroup(string name, [NotNullWhen(true)] out IEnumerable<MethodModel>? methods) =>
       _groups.TryGetValue(name, out methods);

    internal void AddIndex(Type index) =>
        _index[index] = _models.Where(m => m is ICustomAttributesModel member && member.CustomAttributes.ContainsKey(index));

    public IEnumerable<MethodModel> Having<TAttribute>() where TAttribute : Attribute =>
       Having(typeof(TAttribute));

    public IEnumerable<MethodModel> Having(Type attributeType) =>
        _index.TryGetValue(attributeType, out var result) ? result : [];

    public IEnumerator<MethodModel> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
