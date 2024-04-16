using Do.Test.Orm;
using Do.Testing;

namespace Do.Test;

public static class ParentExtensions
{
    public static Parent AParent(this Stubber giveMe,
        string? name = default,
        bool withChild = false
    )
    {
        name ??= giveMe.AString();

        var result = giveMe.A<Parent>().With(name);
        if (withChild)
        {
            result.AddChild();
        }

        return result;
    }
}