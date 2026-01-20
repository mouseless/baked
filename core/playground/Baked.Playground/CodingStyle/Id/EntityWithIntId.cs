using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle;

public class EntityWithIntId(IEntityContext<EntityWithIntId> _context)
{
    public Id Id { get; private set; } = default!;

    public EntityWithIntId With()
    {
        return _context.Insert(this);
    }
}

public class EntityWithIntIds(IQueryContext<EntityWithIntId> _context)
{
    public List<EntityWithIntId> By() =>
        _context.By(c => true);
}