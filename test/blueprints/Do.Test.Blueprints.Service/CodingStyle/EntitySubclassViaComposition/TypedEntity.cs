using Do.Orm;

namespace Do.Test.CodingStyle.EntitySubclassViaComposition;

public class TypedEntity(IEntityContext<TypedEntity> _context)
{
    protected TypedEntity() : this(default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual TypedEntityType Type { get; protected set; } = default!;

    protected internal virtual TypedEntity With(TypedEntityType type)
    {
        Type = type;

        return _context.Insert(this);
    }

    public virtual void Delete() =>
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