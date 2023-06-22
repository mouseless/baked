namespace Do.Test;

public abstract class Spec
{
    protected Stubber GiveMe { get; private set; } = default!;

    [SetUp]
    public virtual void SetUp()
    {
        GiveMe = new();
    }

    public sealed class Stubber { }
}
