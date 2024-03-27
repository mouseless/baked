namespace Do.Domain.Model;

public record DomainModel(
    ModelCollection<TypeModel> ReflectedTypes,
    ModelCollection<TypeModel> ReferencedTypes
)
{
    public TypeModel this[Type type] => 
        this[TypeModel.IdFrom(type)];

    public TypeModel this[string typeId]
    {
        get
        {
            if (ReflectedTypes.TryGetValue(typeId, out var model)) { return model; }
            if (ReflectedTypes.TryGetValue(typeId, out model)) { return model; }

            throw new KeyNotFoundException(typeId);
        }
    }
}