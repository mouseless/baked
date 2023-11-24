using Do.Orm;

namespace EventScheduler;

public class Contact
{
    readonly IEntityContext<Contact> _context = default!;

    protected Contact() { }

    public Contact(IEntityContext<Contact> entityContext)
    {
        _context = entityContext;
    }

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

public class Contacts
{
    readonly IQueryContext<Contact> _context = default!;

    public Contacts(IQueryContext<Contact> context) =>
        _context = context;

    public List<Contact> All() =>
        _context.All();
}