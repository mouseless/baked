using Baked.Orm;

namespace Baked.Playground.CodingStyle.PrimitiveViaParsable;

public class EntityWithPrimitive(IEntityContext<EntityWithPrimitive> _context)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public Value Value { get; private set; } = default!;
    public Value? ValueNullable { get; private set; } = default!;
    public Value? ValueNullableNull { get; private set; } = default!;

    public EntityWithPrimitive With(Value value)
    {
        Value = value;
        ValueNullable = value;

        return _context.Insert(this);
    }
}

public class EntityWithPrimitives(IQueryContext<EntityWithPrimitive> _context)
{
    public List<EntityWithPrimitive> By(
        Value? value = default
    ) => _context.By(
        whereIf: [(value is not null, ewvt => ewvt.Value == value)]
    );
}