using Do.Communication;
using Do.Database;
using Newtonsoft.Json;

namespace Do.Test;

public class Singleton(
    TimeProvider _timeProvider,
    Func<OperationWithGenericParameter<Entity>> _newOperationWithGenericParameter
) : SingletonBase, IInterface
{
    public override DateTime GetTime() =>
        _timeProvider.GetNow();

    public string TestOperationWithGenericParameter(string parameter)
    {
        return _newOperationWithGenericParameter()
            .With(parameter)
            .Execute();
    }
}
