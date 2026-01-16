using Baked.Orm;

namespace Mouseless.EventScheduler;

public class Meeting(
    IEntityContext<Meeting> _context,
    Func<MeetingContact> _newMeetingContact,
    MeetingContacts _meetingContacts
)
{
    public Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public DateTime Date { get; private set; } = default!;

    public Meeting With(string name, DateTime date)
    {
        Name = name;
        Date = date;

        return _context.Insert(this);
    }

    public List<Contact> GetContacts() =>
        [.. _meetingContacts.ByMeeting(this).Select(mc => mc.Contact)];

    public void AddContact(Contact contact)
    {
        _newMeetingContact().With(this, contact);
    }

    public void Delete()
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
