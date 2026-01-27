using Baked.Business;
using Baked.Playground.Orm;

namespace Baked.Playground.CodingStyle.EntityExtensionViaComposition;

public class EntityExtension
{
    Entity Entity { get; set; } = default!;

    internal EntityExtension With(Entity entity)
    {
        Entity = entity;

        return this;
    }

    public async Task IncrementInt32() =>
        await Entity.Update(int32: Entity.Int32 + 1);

    public async Task IncrementBy(EntityExtension other) =>
        await Entity.Update(int32: Entity.Int32 + other.Entity.Int32);

    public async Task IncrementByAll(
        IEnumerable<EntityExtension> extensions,
        EntityExtension[] moreExtensions,
        List<EntityExtension> evenMoreExtensions
    ) => await Entity.Update(int32:
            Entity.Int32 +
            extensions.Sum(e => e.Entity.Int32) +
            moreExtensions.Sum(e => e.Entity.Int32) +
            evenMoreExtensions.Sum(e => e.Entity.Int32)
        );

    public static implicit operator EntityExtension(Entity entity) =>
        entity.Cast().To<EntityExtension>();
}

public class EntityExtensions(Func<EntityExtension> _newEntityExtension)
    : ICasts<Entity, EntityExtension>
{
    EntityExtension ICasts<Entity, EntityExtension>.To(Entity from) =>
        _newEntityExtension().With(from);
}