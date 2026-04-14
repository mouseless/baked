using Baked.Orm;

namespace Baked.Playground.CodingStyle.Id;

public class EntityWithAssignedId(IEntityContext<EntityWithAssignedId> _context)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    // This property is to test foreign key column mapping
    public EntityWithAutoIncrementId? Foreign { get; private set; } = default!;

    public EntityWithAssignedId With(Baked.Business.Id id)
    {
        Id = id;

        return _context.Insert(this);
    }
}