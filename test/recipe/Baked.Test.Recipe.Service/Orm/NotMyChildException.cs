using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class NotMyChildException(Child child)
    : HandledException($"Child#{child.Id} does not belong this parent");