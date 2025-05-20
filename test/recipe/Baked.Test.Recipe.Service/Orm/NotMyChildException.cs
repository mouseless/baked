using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class NotMyChildException(Child child)
    : HandledException($"Child#{child.Id} does not belong this parent")
{
    public override string LKey => "child_ID_does_not_belong_this_parent";
    public override string[] LParams => [child.Id.ToString()];
}