using Baked.Orm;

namespace Mouseless.EventScheduler;

public class Meeting(IEntityContext<Meeting> _context, Func<MeetingContact> _newMeetingContact, MeetingContacts _meetingContacts)
{
    protected Meeting() : this(default!, default!, default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual string Name { get; protected set; } = default!;
    public virtual DateTime Date { get; protected set; } = default!;

    public virtual Meeting With(string name, DateTime date)
    {
        Name = name;
        Date = date;

        return _context.Insert(this);
    }

    public virtual List<Contact> GetContacts() =>
        _meetingContacts.ByMeeting(this).Select(mc => mc.Contact).ToList();

    public virtual void AddContact(Contact contact)
    {
        _newMeetingContact().With(this, contact);
    }

    public virtual void Delete()
    {
        var meetingContacts = _meetingContacts.ByMeeting(this);
        foreach (var item in meetingContacts)
        {
            item.Delete();
        }

        _context.Delete(this);
    }
}

public class Meetings(IQueryContext<Meeting> _context)
{
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