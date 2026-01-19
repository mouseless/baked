using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle.EntitySubclassViaComposition;

public class TypedEntity(IEntityContext<TypedEntity> _context)
{
    public Id Id { get; private set; } = default!;
    public TypedEntityType Type { get; private set; } = default!;

    internal TypedEntity With(TypedEntityType type)
    {
        Type = type;

        return _context.Insert(this);
    }

    public void Delete() =>
        _context.Delete(this);
}

public class TypedEntities(IQueryContext<TypedEntity> _context)
{
    public List<TypedEntity> By() =>
        _context.By(_ => true);

    public TypedEntity SingleByType(TypedEntityType type,
        bool throwNotFound = false
    ) => _context.SingleBy(te => te.Type == type) ?? throw RecordNotFoundException.For<TypedEntity>(nameof(type), type, notFound: throwNotFound);
}