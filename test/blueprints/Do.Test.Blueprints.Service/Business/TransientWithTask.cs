namespace Do.Test.Business;

public class TransientWithTask
{
    internal Task<TransientWithTask> With() => Task.FromResult(this);
}
