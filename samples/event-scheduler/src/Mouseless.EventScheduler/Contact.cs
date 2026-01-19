using Baked.Business;
using Baked.Orm;

namespace Mouseless.EventScheduler;

public class Contact(IEntityContext<Contact> _context)
{
    public Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Contact With(string name)
    {
        Name = name;

        return _context.Insert(this);
    }

    public void Update(string name)
    {
        Name = name;
    }
}

public class Contacts(IQueryContext<Contact> _context)
{
    public List<Contact> By(
        string? name = default
    ) => _context.By(c => name == default || c.Name == name);
}
