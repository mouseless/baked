using Do.ExceptionHandling;

namespace Do.Test.Orm;

public class MustBeUniqueException(string propertyName)
    : HandledException($"{propertyName} should be unique")
{ }
