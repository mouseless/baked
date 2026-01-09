using Baked.Orm;

namespace Baked.Playground.Orm;

public class Parent(IEntityContext<Parent> _context, Func<Child> _newChild, Children _childEntities)
    : IParentInterface
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string? Description { get; private set; } = default!;

    public Parent With(string name, string surname)
    {
        Name = name;
        Surname = surname;

        return _context.Insert(this);
    }

    public List<Child> GetChildren()
    {
        return _childEntities.ByParent(this);
    }

    public void AddChild(string name)
    {
        _newChild().With(this, name);
    }

    public void Update(
        string? name = default,
        string? surname = default,
        string? description = default
    )
    {
        Name = name ?? Name;
        Surname = surname ?? Surname;
        Description = description ?? Description;
    }

    public void RemoveChild(Child child)
    {
        if (child.Parent != this) { throw new NotMyChildException(child); }

        child.Delete();
    }

    public void Delete()
    {
        foreach (var child in GetChildren())
        {
            child.Delete();
        }

        _context.Delete(this);
    }

    bool IsContextNull() =>
        _context is null;

    bool IParentInterface.IsContextNull() => IsContextNull();
}

public class Parents(IQueryContext<Parent> _context)
{
    public List<Parent> By(
        string? name = default,
        bool asc = false,
        bool desc = false,
        int? take = default,
        int? skip = default
    )
    {
        return
            asc ? _context.By(p => name == default || p.Name == name, orderBy: p => p.Name, take: take, skip: skip) :
            desc ? _context.By(p => name == default || p.Name == name, orderByDescending: p => p.Name, take: take, skip: skip) :
            _context.By(p => name == default || p.Name == name, take: take, skip: skip);
    }

    public List<Parent> ByName(string contains,
        bool asc = false,
        bool desc = false,
        int? take = default,
        int? skip = default
    )
    {
        return
            asc ? _context.By(where: p => p.Name.Contains(contains), orderBy: p => p.Name, take: take, skip: skip) :
            desc ? _context.By(where: p => p.Name.Contains(contains), orderByDescending: p => p.Name, take: take, skip: skip) :
            _context.By(where: p => p.Name.Contains(contains), take: take, skip: skip);
    }
}