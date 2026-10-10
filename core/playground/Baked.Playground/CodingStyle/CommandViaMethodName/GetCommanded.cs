namespace Baked.Playground.CodingStyle.CommandViaMethodName;

public class GetCommanded
{
    public void Execute()
    {
        PrivateMethod();
    }

    void PrivateMethod() { }
}