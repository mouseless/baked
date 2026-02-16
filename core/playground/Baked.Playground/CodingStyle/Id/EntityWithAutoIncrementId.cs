using Baked.Orm;

namespace Baked.Playground.CodingStyle.Id;

public class EntityWithAutoIncrementId(IEntityContext<EntityWithAutoIncrementId> _context)
{
    // `Id` is named differently to test custom id property name and foreign key mappings
    public Baked.Business.Id PrimaryKey { get; private set; } = default!;

    public EntityWithAutoIncrementId With()
    {
        return _context.Insert(this);
    }

    public EntityWithAutoIncrementId GetTestCustomIdPropertyName(EntityWithAutoIncrementId other) =>
        other;

    public EntityWithAutoIncrementId TestCustomIdPropertyName(EntityWithAutoIncrementId other) =>
        other;
}

public class EntityWithAutoIncrementIds(IQueryContext<EntityWithAutoIncrementId> _context)
{
    public List<EntityWithAutoIncrementId> By() =>
        _context.By(c => true);
}