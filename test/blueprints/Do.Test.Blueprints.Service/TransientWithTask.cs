namespace Do.Test;

public class TransientWithTask
{
    public Task<TransientWithTask> With() => Task.FromResult(this);

    public void Execute() { }
}
