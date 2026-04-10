namespace Baked.Playground.CodingStyle.NamespaceAsRoute;

public class RouteSample
{
    public Baked.Business.Id Id { get; private set; } = default!;

    public RouteSample With(Baked.Business.Id id)
    {
        Id = id;

        return this;
    }

    public void Method() { }
}