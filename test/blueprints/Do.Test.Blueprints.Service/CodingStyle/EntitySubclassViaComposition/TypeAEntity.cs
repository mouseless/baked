using Do.Business;
using Do.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Do.Test.CodingStyle.EntitySubclassViaComposition;

public class TypeAEntity(ILogger<TypeAEntity> _logger)
{
    Entity _entity = default!;

    internal TypeAEntity With(Entity entity)
    {
        if (entity.Unique != "TypeA") { throw new InvalidOperationException("entity is not TypeA"); }

        _entity = entity;

        return this;
    }

    public void OperateOnTypeA() =>
        _logger.LogInformation($"Operating on type a for entity#{_entity.Id}");

    public static explicit operator TypeAEntity(Entity entity) => entity.Cast().To<TypeAEntity>();
}

public class TypeAEntities(Func<TypeAEntity> _newTypeAEntity)
    : ICasts<Entity, TypeAEntity>
{
    TypeAEntity ICasts<Entity, TypeAEntity>.To(Entity from) =>
        _newTypeAEntity().With(from);
}