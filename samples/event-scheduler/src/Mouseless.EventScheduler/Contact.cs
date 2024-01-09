using Do.Orm;

namespace Mouseless.EventScheduler;

public class Contact(IEntityContext<Contact> _context)
{
    protected Contact() : this(default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual string Name { get; protected set; } = default!;

    public virtual Contact With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public virtual void Edit(string name)
    {
        Name = name;
    }
}

public class Contacts(IQueryContext<Contact> _context)
{
    public List<Contact> All() =>
        _context.All();
}