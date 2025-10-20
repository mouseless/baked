using Baked.Business;

namespace Mouseless.EventScheduler;

public class DeleteMeetingContact(MeetingContacts _meetingContacts)
{
    Meeting _meeting = default!;

    internal DeleteMeetingContact With(Meeting meeting)
    {
        _meeting = meeting;

        return this;
    }

    public void Execute(Contact contact)
    {
        var result = _meetingContacts.SingleBy(_meeting, contact);

        result?.Delete();
    }

    public static implicit operator DeleteMeetingContact(Meeting meeting) =>
            meeting.Cast().To<DeleteMeetingContact>();
}

public class DeleteMeetingContacts(Func<DeleteMeetingContact> _newDeleteMeetingContact)
    : ICasts<Meeting, DeleteMeetingContact>
{
    DeleteMeetingContact ICasts<Meeting, DeleteMeetingContact>.To(Meeting from) =>
        _newDeleteMeetingContact().With(from);
}