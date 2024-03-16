using Do.ExceptionHandling;

namespace Do.Test;

public class MustBeUniqException(string propertyName)
    : HandledException($"{propertyName} should be unique") { }
