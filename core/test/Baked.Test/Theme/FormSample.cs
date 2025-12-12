using Baked.Authorization;
using Baked.Test.Orm;

namespace Baked.Test.Theme;

// TODO - review this in form components
[AllowAnonymous]
public class FormSample(Parents _parents, Func<Parent> _newParent)
{
    public void NewParent(string name)
    {
        _newParent().With(name);
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