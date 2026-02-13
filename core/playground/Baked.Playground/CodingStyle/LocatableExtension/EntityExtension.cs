using Baked.Business;
using Baked.Playground.Orm;

namespace Baked.Playground.CodingStyle.LocatableExtension;

public class EntityExtension
{
    Entity _entity = default!;

    public Baked.Business.Id Id => _entity.Id;

    internal EntityExtension With(Entity entity)
    {
        _entity = entity;

        return this;
    }

    public async Task IncrementInt32() =>
        await _entity.Update(int32: _entity.Int32 + 1);

    public async Task IncrementBy(EntityExtension other) =>
        await _entity.Update(int32: _entity.Int32 + other._entity.Int32);

    public async Task IncrementByAll(
        IEnumerable<EntityExtension> extensions,
        EntityExtension[] moreExtensions,
        List<EntityExtension> evenMoreExtensions
    ) => await _entity.Update(int32:
            _entity.Int32 +
            extensions.Sum(e => e._entity.Int32) +
            moreExtensions.Sum(e => e._entity.Int32) +
            evenMoreExtensions.Sum(e => e._entity.Int32)
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