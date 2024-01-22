namespace Do.Test;

public class OperationObject : ITransient
{
    public OperationObject With() => this;

    public void Process() { }
}
