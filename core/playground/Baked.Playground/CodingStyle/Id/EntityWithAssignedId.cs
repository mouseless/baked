using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle;

public class EntityWithAssignedId(IEntityContext<EntityWithAssignedId> _context)
{
    public Id Id { get; private set; } = default!;
    // This property is to test foreign key column mapping
    public EntityWithAutoIncrementId? Foreign { get; set; } = default!;

    public EntityWithAssignedId With(Id id)
    {
        Id = id;

        return _context.Insert(this);
    }
}

public class EntityWithAssignedIds(IQueryContext<EntityWithAssignedId> _context)
{
    public List<EntityWithAssignedId> By() =>
        _context.By(c => true);
}