namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientWithCustomIdProperty
{
    public Baked.Business.Id Uid { get; private set; } = default!;

    public RichTransientWithCustomIdProperty With(Baked.Business.Id uid)
    {
        Uid = uid;

        return this;
    }

    public RichTransientWithCustomIdProperty GetTestCustomIdPropertyName(RichTransientWithCustomIdProperty other) =>
        other;

    public RichTransientWithCustomIdProperty TestCustomIdPropertyName(RichTransientWithCustomIdProperty other) =>
        other;
}