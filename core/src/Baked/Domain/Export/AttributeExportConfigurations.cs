namespace Baked.Domain.Export;

public class AttributeExportConfigurations : IEnumerable<KeyValuePair<string, AttributeExportConfiguration>>
{
    readonly Dictionary<string, AttributeExportConfiguration> _exports = new();

    public void Build(string key, Action<AttributeExportConfiguration> configure)
    {
        if (!_exports.TryGetValue(key, out var export))
        {
            export = _exports[key] = new(key);
        }

        configure(export);
    }

    public IEnumerator<KeyValuePair<string, AttributeExportConfiguration>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, AttributeExportConfiguration>>)_exports).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_exports).GetEnumerator();
}