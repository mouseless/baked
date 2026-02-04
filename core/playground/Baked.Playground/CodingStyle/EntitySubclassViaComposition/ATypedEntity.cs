using Baked.Business;
using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.EntitySubclassViaComposition;

public class ATypedEntity(ILogger<ATypedEntity> _logger, Func<TypedEntity> _newTypedEntity)
{
    TypedEntity _entity = default!;

    public Baked.Business.Id Id => _entity.Id;

    public ATypedEntity With() =>
        With(_newTypedEntity().With(TypedEntityType.A));

    internal ATypedEntity With(TypedEntity entity)
    {
        if (entity.Type != TypedEntityType.A) { throw new InvalidOperationException("entity is not A"); }

        _entity = entity;

        return this;
    }

    public void OperateOnA() =>
        _logger.LogInformation($"Operating on A for entity#{_entity.Id}");

    public static explicit operator ATypedEntity(TypedEntity entity) => entity.Cast().To<ATypedEntity>();
}

public class ATypedEntities(Func<ATypedEntity> _newATypedEntity)
    : ICasts<TypedEntity, ATypedEntity>
{
    ATypedEntity ICasts<TypedEntity, ATypedEntity>.To(TypedEntity from) =>
        _newATypedEntity().With(from);
}