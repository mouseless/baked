namespace Baked.Domain.Export;

public class AttributeExportCollection : IEnumerable<KeyValuePair<string, AttributeExport>>
{
    readonly Dictionary<string, AttributeExport> _exports = new();

    public void Build(string key, Action<AttributeExport> configure)
    {
        if (!_exports.TryGetValue(key, out var export))
        {
            export = _exports[key] = new(key);
        }

        configure(export);
    }

    public AttributeExport Get(string key)
    {
        if (!_exports.TryGetValue(key, out var export))
        {
            export = _exports[key] = new(key);
        }

        return export;
    }

    public IEnumerator<KeyValuePair<string, AttributeExport>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, AttributeExport>>)_exports).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_exports).GetEnumerator();
}