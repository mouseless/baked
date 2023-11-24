using Do.Orm;

namespace Mouseless.EventScheduler;

public class MeetingContact
{
    readonly IEntityContext<MeetingContact> _context = default!;

    protected MeetingContact() { }
    public MeetingContact(IEntityContext<MeetingContact> entityContext)
    {
        _context = entityContext;
    }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual Meeting Meeting { get; protected set; } = default!;
    public virtual Contact Contact { get; protected set; } = default!;

    protected internal virtual MeetingContact With(Meeting meeting, Contact contact)
    {
        Meeting = meeting;
        Contact = contact;

        return _context.Insert(this);
    }

    protected internal virtual void Delete()
    {
        _context.Delete(this);
    }
}

public class MeetingContacts
{
    IQueryContext<MeetingContact> _context;

    public MeetingContacts(IQueryContext<MeetingContact> context) =>
        _context = context;

    internal List<MeetingContact> ByMeeting(Meeting meeting) =>
        _context.By(mc => mc.Meeting == meeting);
}
