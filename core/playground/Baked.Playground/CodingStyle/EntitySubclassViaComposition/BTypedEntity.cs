using Baked.Business;
using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.EntitySubclassViaComposition;

public class BTypedEntity(ILogger<BTypedEntity> _logger, Func<TypedEntity> _newTypedEntity)
{
    TypedEntity _entity = default!;

    public Guid Id => _entity.Id;

    public BTypedEntity With() =>
        With(_newTypedEntity().With(TypedEntityType.B));

    internal BTypedEntity With(TypedEntity entity)
    {
        if (entity.Type != TypedEntityType.B) { throw new InvalidOperationException("entity is not B"); }

        _entity = entity;

        return this;
    }

    public void OperateOnB() =>
        _logger.LogInformation($"Operating on B for entity#{_entity.Id}");

    public static explicit operator BTypedEntity(TypedEntity entity) => entity.Cast().To<BTypedEntity>();
}

public class BTypedEntities(Func<BTypedEntity> _newBTypedEntity)
    : ICasts<TypedEntity, BTypedEntity>
{
    BTypedEntity ICasts<TypedEntity, BTypedEntity>.To(TypedEntity from) =>
        _newBTypedEntity().With(from);
}