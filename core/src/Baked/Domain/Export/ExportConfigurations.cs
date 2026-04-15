namespace Baked.Domain.Export;

public class ExportConfigurations : IEnumerable<KeyValuePair<string, ExportConfiguration>>
{
    readonly Dictionary<string, ExportConfiguration> _exports = new();

    public void Build(string key, Action<ExportConfiguration> configure)
    {
        if (!_exports.TryGetValue(key, out var export))
        {
            export = _exports[key] = new(key);
        }

        configure(export);
    }

    public IEnumerator<KeyValuePair<string, ExportConfiguration>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, ExportConfiguration>>)_exports).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_exports).GetEnumerator();
}