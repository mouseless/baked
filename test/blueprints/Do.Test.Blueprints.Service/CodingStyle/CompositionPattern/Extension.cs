using Do.Business;
using Do.Test.Orm;

namespace Do.Test.CodingStyle.CompositionPattern;

public class Extension(Func<Entity> _newEntity)
{
    Entity _entity = default!;

    public Guid Id => _entity.Id;
    public string Name => _entity.String;

    public Extension With(string name) =>
        With(_newEntity().With(@string: name));

    internal Extension With(Entity entity)
    {
        _entity = entity;

        return this;
    }

    public static implicit operator Extension(Entity entity) => entity.Cast().To<Extension>();
    public static implicit operator Entity(Extension composite) => composite._entity;

    public async Task UpdateName(string newName) =>
        await _entity.Update(@string: newName);
}

public class Extensions(Func<Extension> _newComposite)
    : ICasts<Entity, Extension>
{
    Extension ICasts<Entity, Extension>.To(Entity from) =>
        _newComposite().With(from);
}