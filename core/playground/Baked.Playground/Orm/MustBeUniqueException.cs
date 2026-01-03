using Baked.ExceptionHandling;

namespace Baked.Playground.Orm;

public class MustBeUniqueException(string propertyName)
    : HandledException("{0} should be unique",
        extraData: new() { ["name"] = propertyName }
    );