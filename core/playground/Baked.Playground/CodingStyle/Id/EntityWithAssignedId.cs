using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle;

public class EntityWithAssignedId(IEntityContext<EntityWithAssignedId> _context)
{
    public Id Id { get; private set; } = default!;

    public EntityWithAssignedId With(string id)
    {
        Id = Id.Parse(id);

        return _context.Insert(this);
    }
}

public class EntityWithAssignedIds(IQueryContext<EntityWithAssignedId> _context)
{
    public List<EntityWithAssignedId> By() =>
        _context.By(c => true);
}