namespace Do.Domain.Model;

public record DomainModel(
    ModelCollection<TypeModel> ReflectedTypes,
    ModelCollection<TypeModel> ReferencedTypes
)
{
    public TypeModel this[Type type] => ReflectedTypes[type]; // TODO fix and add referenced type lookup
    public TypeModel this[string typeId] => ReflectedTypes[typeId]; // TODO fix and add referenced type lookup
}