namespace Baked.Playground.Business;

public class GenericClass<T>
{
    internal async Task<GenericClass<T>> With()
    {
        await Task.Delay(100);

        Method(new());

        return this;
    }

    void Method(Dictionary<int, PrivateRecord> _)
    { }

    record PrivateRecord(string Text);
}