using Baked.Test.Orm;
using Baked.Testing;

namespace Baked.Test;

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
            result.AddChild(giveMe.AString());
        }

        return result;
    }
}