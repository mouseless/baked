namespace Do.Domain.Model;

public class AttributeIndexer(Type _attributeType)
{
    string IndexId => TypeModel.IdFrom(_attributeType);

    internal void Execute<T>(ModelIndex<T> index, T model) where T : IModelWithMetadata
    {
        if (model.CustomAttributes.Contains(IndexId))
        {
            if (!index.ContainsKey(IndexId))
            {
                index[IndexId] = [];
            }

            index[IndexId].TryAdd(model);
        }
    }
}
