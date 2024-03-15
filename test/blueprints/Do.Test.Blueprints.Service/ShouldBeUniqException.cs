using Do.ExceptionHandling;

namespace Do.Test;

public class ShouldBeUniqException(string propertyName)
    : HandledException($"{propertyName} should be unique") { }
