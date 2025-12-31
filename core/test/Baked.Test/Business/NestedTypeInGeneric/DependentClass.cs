namespace Baked.Test.Business;

public class DependentClass(Func<GenericClass<DataType>> _newDependency)
{
    public async Task<GenericClass<DataType>> Method() =>
        await _newDependency().With();
}