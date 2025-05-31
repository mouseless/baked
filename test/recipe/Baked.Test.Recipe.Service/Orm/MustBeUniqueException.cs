using Baked.ExceptionHandling;

namespace Baked.Test.Orm;

public class MustBeUniqueException(string propertyName)
    : HandledException($"{propertyName} should be unique")
{
    public override string LKey => "NAME_should_be_unique";
    public override string[] LParams => [propertyName];
}