using Do.Orm;

namespace EventScheduler;

public class Meeting
{
    readonly IEntityContext<Meeting> _context = default!;

    protected Meeting() { }
    public Meeting(IEntityContext<Meeting> context) =>
        _context = context;

    public virtual Guid Id { get; protected set; } = default!;
    public virtual string Name { get; protected set; } = default!;
    public virtual DateTime Date { get; protected set; } = default!;

    public virtual Meeting With(string name, DateTime date)
    {
        Name = name;
        Date = date;

        return _context.Insert(this);
    }

    public virtual void Delete()
    {
        _context.Delete(this);
    }
}

public class Meetings
{
    readonly IQueryContext<Meeting> _context;

    public Meetings(IQueryContext<Meeting> context) =>
        _context = context;

    public List<Meeting> By(
        DateTime? before = default,
        DateTime? after = default,
        int take = 10
    ) => _context.By(
            where: e =>
                (before == default || e.Date <= before) &&
                (after == default || e.Date >= after),
            orderByDescending: e => e.Date,
            take: take
        );
}
