using Do.ExceptionHandling;

namespace Do.Test.Orm;

public class NotMyChildException(Child child)
    : HandledException($"Child#{child.Id} does not belong this parent")
{ }