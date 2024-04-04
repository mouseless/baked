using Do.Communication;
using Do.Database;
using Newtonsoft.Json;

namespace Do.Test;

public class Singleton(
    TimeProvider _timeProvider,
    Func<OperationWithGenericParameter<Entity>> _newOperationWithGenericParameter
) : SingletonBase(_timeProvider), IInterface
{
    public string TestOperationWithGenericParameter(string parameter)
    {
        return _newOperationWithGenericParameter()
            .With(parameter)
            .Execute();
    }
}
