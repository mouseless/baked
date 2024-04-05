using Do.Test.Business;

namespace Do.Test;

public class Operation : IOperation
{
    public Operation With() => this;

    public void Execute() { }
}