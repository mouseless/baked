using Do.Test.Orm;

namespace Do.Test.Business;

public class Singleton(
    TimeProvider _timeProvider,
    Func<OperationWithGenericParameter<Entity>> _newOperationWithGenericParameter
) : SingletonBase, IInterface
{
    public override DateTime GetTime() =>
        _timeProvider.GetNow();

    public string OperationWithGenericParameter(string parameter)
    {
        return _newOperationWithGenericParameter()
            .With(parameter)
            .Execute();
    }
}
