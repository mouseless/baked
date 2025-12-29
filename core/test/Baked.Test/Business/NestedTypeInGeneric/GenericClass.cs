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
