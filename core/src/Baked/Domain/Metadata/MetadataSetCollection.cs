namespace Baked.Domain.Metadata;

public class MetadataSetCollection : IEnumerable<KeyValuePair<string, MetadataSet>>
{
    readonly Dictionary<string, MetadataSet> _sets = new();

    public MetadataSet GetOrCreate(string key) =>
        _sets.TryGetValue(key, out var metadataSet) ? metadataSet : _sets[key] = new(key);

    public IEnumerator<KeyValuePair<string, MetadataSet>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, MetadataSet>>)_sets).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_sets).GetEnumerator();
}