using Do.Orm;

namespace Do.Test;

public class ParentEntity(IEntityContext<ParentEntity> _context, ChildEntities _childEntities, TimeProvider _timeProvider)
{
    protected ParentEntity() : this(default!, default!, default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual DateTime DateTime { get; protected set; } = default!;

    public virtual ParentEntity With(
        DateTime? dateTime = default
    )
    {
        DateTime = dateTime ?? _timeProvider.GetNow();

        return _context.Insert(this);
    }

    public virtual List<ChildEntity> GetChildren(bool reverse = default, int? skip = default, int? take = default) =>
        _childEntities.ByParent(Id, reverse, skip, take);
}

public class ParentEntities(IQueryContext<ParentEntity> _context)
{
    public List<ParentEntity> All(bool reverse = default, int? skip = default, int? take = default)
    {
        return _context.All<DateTime>(
            orderBy: reverse is true ? null : e => e.DateTime,
            orderByDescending: reverse is false ? null : e => e.DateTime,
            skip: skip,
            take: take
            );
    }
}