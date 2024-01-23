using Do.Orm;

namespace Do.Test;

public class ChildEntity(IEntityContext<ChildEntity> _context, TimeProvider _timeProvider)
{
    protected ChildEntity() : this(default!, default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual ParentEntity Parent { get; protected set; } = default!;
    public virtual DateTime DateTime { get; protected set; } = default!;

    public virtual ChildEntity With(
        ParentEntity parent,
        DateTime? dateTime = default
    )
    {
        Parent = parent;
        DateTime = dateTime ?? _timeProvider.GetNow();

        return _context.Insert(this);
    }
}

public class ChildEntities(IQueryContext<ChildEntity> _context)
{
    public List<ChildEntity> All(int? skip = default, int? take = default) =>
        _context.All(skip, take);

    public List<ChildEntity> ByParent(Guid parentId, bool reverse = default, int? skip = default, int? take = default)
    {
        return _context.By<DateTime>(
            where: e => e.Parent.Id == parentId,
            orderBy: reverse is true ? null : e => e.DateTime,
            orderByDescending: reverse is false ? null : e => e.DateTime,
            skip: skip,
            take: take
            );
    }

    public ChildEntity? FirstByParent(Guid parentId, bool reverse = default)
    {
        return _context.FirstBy<DateTime>(
            where: e => e.Parent.Id == parentId,
            orderBy: reverse is true ? null : e => e.DateTime,
            orderByDescending: reverse is false ? null : e => e.DateTime
            );
    }
}
