using Baked.Orm;

namespace Baked.Playground.CodingStyle.ValueType;

public class EntityWithValueType(IEntityContext<EntityWithValueType> _context)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public Value Value { get; private set; } = default!;
    public Value? ValueNullable { get; private set; } = default!;
    public Value? ValueNullableNull { get; private set; } = default!;

    public EntityWithValueType With(Value value)
    {
        Value = value;
        ValueNullable = value;

        return _context.Insert(this);
    }
}

public class EntityWithValueTypes(IQueryContext<EntityWithValueType> _context)
{
    public List<EntityWithValueType> By(
        Value? value = default
    ) => _context.By(
        whereIf: [(value is not null, ewvt => ewvt.Value == value)]
    );
}