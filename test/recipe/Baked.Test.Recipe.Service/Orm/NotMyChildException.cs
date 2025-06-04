using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class NotMyChildException(Child child)
    : HandledException("Child_ID_does_not_belong_this_parent",
        extraData: new() { { "localizerParams", new object?[] { child.Id.ToString() } } }
    );