using Baked.Orm;

namespace Baked.Playground.CodingStyle.Id;

public class EntityWithAutoIncrementId(IEntityContext<EntityWithAutoIncrementId> _context)
{
    // `Id` is named differently to test `Id` and `ForeignKey` mapping
    public Baked.Business.Id PrimaryKey { get; private set; } = default!;

    public EntityWithAutoIncrementId With()
    {
        return _context.Insert(this);
    }
}

public class EntityWithAutoIncrementIds(IQueryContext<EntityWithAutoIncrementId> _context)
{
    public List<EntityWithAutoIncrementId> By() =>
        _context.By(c => true);
}