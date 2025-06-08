using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class MustBeUniqueException(string propertyName)
    : HandledException("NAME_should_be_unique",
        extraData: new() { ["name"] = propertyName }
    );