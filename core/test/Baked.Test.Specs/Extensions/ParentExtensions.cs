using Baked.Test.Orm;
using Baked.Testing;

namespace Baked.Test;

public static class ParentExtensions
{
    public static Parent AParent(this Stubber giveMe,
        string? name = default,
        string? surname = default,
        bool withChild = false
    )
    {
        name ??= giveMe.AString();
        surname ??= giveMe.AString();

        var result = giveMe.A<Parent>().With(name, surname);
        if (withChild)
        {
            result.AddChild(giveMe.AString());
        }

        return result;
    }
}