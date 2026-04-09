namespace Baked.Domain.Metadata;

public class MetadataSetConfigurationCollection : IEnumerable<KeyValuePair<string, MetadataSetConfiguration>>
{
    readonly Dictionary<string, MetadataSetConfiguration> _sets = new();

    public MetadataSetConfiguration GetOrCreate(string key) =>
        _sets.TryGetValue(key, out var metadataSet) ? metadataSet : _sets[key] = new(key);

    public IEnumerator<KeyValuePair<string, MetadataSetConfiguration>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, MetadataSetConfiguration>>)_sets).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_sets).GetEnumerator();
}