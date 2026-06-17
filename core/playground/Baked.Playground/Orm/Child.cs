using Baked.Authorization;
using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.Orm;

[AllowAnonymous]
public class Child(IEntityContext<Child> _context, Func<Parent> _newParent)
{
    public Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public Parent Parent { get; private set; } = default!;
    internal Parent? InternalParent { get; private set; } = default!;
    public ParentWrapper ParentWrapper => new(Parent);
    public IParentInterface ParentInterface => Parent;

    public Child With(string name, string parentName, string parentSurname)
    {
        Parent = _newParent().With(parentName, parentSurname, Status.Active, Role.Admin);
        Name = name;

        if (parentName == "wrong" || parentSurname == "wrong")
        {
            throw new("Test exception for transaction rollback, expect no inserted parent");
        }

        return _context.Insert(this);
    }

    internal Child With(string name, Parent parent)
    {
        Name = name;
        Parent = parent;

        return _context.Insert(this);
    }

    internal void Delete() =>
        _context.Delete(this);

    public void Update(
        string? name = default,
        Parent? parent = default,
        ParentWrapper? parentWrapper = default
    )
    {
        Name = name ?? Name;
        Parent = parent ?? parentWrapper?.Parent ?? Parent;
    }
}

public class Children(IQueryContext<Child> _context)
{
    internal List<Child> ByParent(Parent parent) =>
        _context.By(e => e.Parent == parent);

    public List<Child> By() =>
        _context.By(c => true);

    public Child? FirstBy(bool fetchParents = true) =>
        _context.Query(fetchParents: fetchParents).FirstOrDefault();
}