using Baked.Authorization;
using Baked.Playground.Orm;

namespace Baked.Playground.Theme;

[AllowAnonymous]
public class FormSample(Parents _parents, Func<Parent> _newParent)
{
    public void NewParent(string surname,
        string? name = default,
        Role? role = default,
        Status? status = default
    )
    {
        name ??= "Dr.";

        _newParent().With(name, surname, status, role);
    }

    public void ClearParents()
    {
        var parents = GetParents();
        foreach (var parent in parents)
        {
            parent.Delete();
        }
    }

    public List<Parent> GetParents(
        string? name = default,
        Sort? sort = default,
        int? take = 10,
        int? skip = 0
    ) => _parents.By(
        name: name,
        take: take,
        skip: skip,
        asc: sort == Sort.Asc,
        desc: sort == Sort.Desc
    );
}