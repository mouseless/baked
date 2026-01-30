using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle;

public class EntityWithAutoIncrementId(IEntityContext<EntityWithAutoIncrementId> _context)
{
    public Id Idd { get; private set; } = default!;

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