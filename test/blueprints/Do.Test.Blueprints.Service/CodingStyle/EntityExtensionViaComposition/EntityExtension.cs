using Do.Business;
using Do.Test.Orm;

namespace Do.Test.CodingStyle.EntityExtensionViaComposition;

public class EntityExtension
{
    Entity _entity = default!;

    internal EntityExtension With(Entity entity)
    {
        _entity = entity;

        return this;
    }

    public async Task IncrementInt32() =>
        await _entity.Update(int32: _entity.Int32 + 1);

    public static implicit operator EntityExtension(Entity entity) =>
        entity.Cast().To<EntityExtension>();
}

public class EntityExtensions(Func<EntityExtension> _newComposite)
    : ICasts<Entity, EntityExtension>
{
    EntityExtension ICasts<Entity, EntityExtension>.To(Entity from) =>
        _newComposite().With(from);
}