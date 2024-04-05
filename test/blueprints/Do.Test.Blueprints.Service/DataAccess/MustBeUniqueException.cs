using Do.ExceptionHandling;

namespace Do.Test.DataAccess;

public class MustBeUniqueException(string propertyName)
    : HandledException($"{propertyName} should be unique")
{ }
