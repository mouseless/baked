using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class MustBeUniqueException(string propertyName)
    : HandledException($"{propertyName} should be unique");