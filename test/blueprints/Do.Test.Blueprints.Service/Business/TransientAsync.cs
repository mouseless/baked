namespace Do.Test.Business;

public class TransientAsync
{
    internal Task<TransientAsync> With() =>
        Task.FromResult(this);
}