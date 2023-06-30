namespace Do.Test;

public abstract class Spec
{
    public Stubber GiveMe { get; private set; } = default!;
    public Mocker MockMe { get; private set; } = default!;

    [SetUp]
    public virtual void SetUp()
    {
        GiveMe = new(this);
        MockMe = new(this);
    }

    public sealed record Stubber(Spec Spec);
    public sealed record Mocker(Spec Spec);
}
