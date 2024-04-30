using Do.Business;
using Do.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Do.Test.CodingStyle.EntitySubclassViaComposition;

public class TypeBEntity(ILogger<TypeBEntity> _logger)
{
    Entity _entity = default!;

    internal TypeBEntity With(Entity entity)
    {
        if (entity.Unique != "TypeB") { throw new InvalidOperationException("entity is not TypeB"); }

        _entity = entity;

        return this;
    }

    public void OperateOnTypeB() =>
        _logger.LogInformation($"Operating on type a for entity#{_entity.Id}");

    public static explicit operator TypeBEntity(Entity entity) => entity.Cast().To<TypeBEntity>();
}

public class TypeBEntities(Func<TypeBEntity> _newTypeBEntity)
    : ICasts<Entity, TypeBEntity>
{
    TypeBEntity ICasts<Entity, TypeBEntity>.To(Entity from) =>
        _newTypeBEntity().With(from);
}