namespace Do.Domain.Model;

public class ModelCache<T> : Dictionary<string, ModelCollection<T>> where T : IModel
{
    public ModelCollection<T> GetOrEmpty(string id) =>
        TryGetValue(id, out var result) ? result : new([]);
}
