using Do.Orm;

namespace Do.Test;

public class EntityWithGuidProperty
{
    readonly IEntityContext<EntityWithGuidProperty> _context = default!;

    protected EntityWithGuidProperty() { }
    public EntityWithGuidProperty(IEntityContext<EntityWithGuidProperty> context) =>
        _context = context;

    public virtual Guid Id { get; protected set; } = default!;
    public virtual Guid GuidProperty { get; protected set; } = default!;
    public virtual EntityWithGuidProperty With(Guid guid)
    {
        GuidProperty = guid;

        return _context.Insert(this);
    }

    public virtual void Update(Guid guid)
    {
        GuidProperty = guid;
    }

    public virtual void Delete()
    {
        _context.Delete(this);
    }
}

public class EntityWithGuidProperties
{
    readonly IQueryContext<EntityWithGuidProperty> _context = default!;

    public EntityWithGuidProperties(IQueryContext<EntityWithGuidProperty> context) =>
        _context = context;

    public EntityWithGuidProperty? ByGuidProperty(Guid guid) => _context.SingleBy(e => e.GuidProperty == guid);
}
