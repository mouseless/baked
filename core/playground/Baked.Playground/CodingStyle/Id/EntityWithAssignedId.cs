using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.CodingStyle;

public class EntityWithAssignedId(IEntityContext<EntityWithAssignedId> _context)
{
    public Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public EntityWithAutoIncrementId? Entity { get; set; } = default!;

    public EntityWithAssignedId With(Id id, string name)
    {
        Id = id;
        Name = name;

        return _context.Insert(this);
    }
}

public class EntityWithAssignedIds(IQueryContext<EntityWithAssignedId> _context)
{
    public List<EntityWithAssignedId> By() =>
        _context.By(c => true);
}