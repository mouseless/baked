using Baked.Orm;

namespace Baked.Playground.CodingStyle.Id;

public class EntityWithAssignedGuidId(IEntityContext<EntityWithAssignedGuidId> _context)
{
    public Baked.Business.Id Id { get; private set; } = default!;

    public EntityWithAssignedGuidId With(Baked.Business.Id id)
    {
        Id = id;

        return _context.Insert(this);
    }
}