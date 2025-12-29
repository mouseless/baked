namespace Baked.Test.Business;

public class GenericClass<T>
{
    internal async Task<GenericClass<T>> With()
    {
        await Task.Delay(1000);

        Method(new());

        return this;
    }

    void Method(Dictionary<int, PrivateRecord> _)
    { }

    record PrivateRecord(string Text);
}

public record DataType(string Type);

public class DependentClass(Func<GenericClass<DataType>> _newDependency)
{
    public async Task<GenericClass<DataType>> Method() =>
        await _newDependency().With();
}