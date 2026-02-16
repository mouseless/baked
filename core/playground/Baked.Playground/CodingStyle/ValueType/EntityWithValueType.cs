using Baked.Business;
using Baked.Orm;

namespace Baked.CodingStyle.ValueType;

public class EntityWithValueType(IEntityContext<EntityWithValueType> _context)
{
    public Id Id { get; private set; } = default!;
    public ValueType ValueType { get; private set; } = default!;
    public ValueType? ValueTypeNullable { get; private set; } = default!;

    public EntityWithValueType With(ValueType valueType,
        ValueType? valueTypeNullable = default
    )
    {
        ValueType = valueType;
        ValueTypeNullable = valueTypeNullable;

        return _context.Insert(this);
    }
}

public class EntityWithValueTypes(IQueryContext<EntityWithValueType> _context)
{
    public List<EntityWithValueType> By() =>
        _context.By();
}