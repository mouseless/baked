using Do.Orm;

namespace Do.Test;

public class Parent(IEntityContext<Parent> _context, Children _childEntities, TimeProvider _timeProvider)
{
    protected Parent() : this(default!, default!, default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual DateTime DateTime { get; protected set; } = default!;

    public virtual Parent With(
        DateTime? dateTime = default
    )
    {
        DateTime = dateTime ?? _timeProvider.GetNow();

        return _context.Insert(this);
    }

    public virtual List<Child> GetChildren(bool reverse = default, int? skip = default, int? take = default) =>
        _childEntities.ByParent(Id, reverse, skip, take);
}

public class Parents(IQueryContext<Parent> _context)
{
    public List<Parent> All(bool reverse = default, int? skip = default, int? take = default)
    {
        return _context.All<DateTime>(
            orderBy: reverse is true ? null : e => e.DateTime,
            orderByDescending: reverse is false ? null : e => e.DateTime,
            skip: skip,
            take: take
            );
    }
}