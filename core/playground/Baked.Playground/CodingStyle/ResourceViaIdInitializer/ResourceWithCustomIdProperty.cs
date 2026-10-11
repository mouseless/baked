namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceWithCustomIdProperty
{
    public Baked.Business.Id Uid { get; private set; } = default!;

    public ResourceWithCustomIdProperty With(Baked.Business.Id uid)
    {
        Uid = uid;

        return this;
    }

    public ResourceWithCustomIdProperty GetTestCustomIdPropertyName(ResourceWithCustomIdProperty other) =>
        other;

    public ResourceWithCustomIdProperty TestCustomIdPropertyName(ResourceWithCustomIdProperty other) =>
        other;
}