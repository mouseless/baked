namespace Do.Domain.Model;

public class DomainMetadataProcessors
{
    public TypeModelProcessors Type { get; } = [];

    public void Execute(KeyedModelCollection<TypeModel> types)
    {
        var typeProcessors = Type.OrderBy(p => p.Order);

        foreach (var type in types)
        {
            foreach (var processor in typeProcessors)
            {
                if (processor.AppliesTo(type))
                {
                    processor.Apply(type);
                }
            }
        }
    }
}
