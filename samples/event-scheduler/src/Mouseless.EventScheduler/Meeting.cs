using Do.Orm;

namespace EventScheduler;

public class Meeting
{
    readonly IEntityContext<Meeting> _context = default!;
    readonly Func<MeetingContact> _newMeetingContact = default!;
    readonly MeetingContacts _meetingContacts = default!;

    protected Meeting() { }
    public Meeting(IEntityContext<Meeting> context, Func<MeetingContact> newMeetingContact, MeetingContacts meetingContacts)
    {
        _context = context;
        _newMeetingContact = newMeetingContact;
        _meetingContacts = meetingContacts;
    }

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
