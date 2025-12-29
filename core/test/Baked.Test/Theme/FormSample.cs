using Baked.Authorization;
using Baked.Test.Orm;

namespace Baked.Test.Theme;

[AllowAnonymous]
public class FormSample(Parents _parents, Func<Parent> _newParent)
{
    public void NewParent(string name, string surname)
    {
        _newParent().With(name, surname);
    }

    public void ClearParents()
    {
        var parents = GetParents();
        foreach (var parent in parents)
        {
            parent.Delete();
        }
    }

    public List<Parent> GetParents() =>
        _parents.By();
}