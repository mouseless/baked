namespace Do.Test;

public class IntegrationSpecRunner
{
    public static void Run(string[] args)
    {
        var type = Type.GetType(GetArgumentValue(args[0], '=')) ?? throw new("Type should have existed");

        ((IIntegrationSpec)(Activator.CreateInstance(type) ?? throw new("Should not be null"))).Run();
    }

    static string GetArgumentValue(string arg, char index) =>
        arg[(arg.IndexOf(index) + 1)..];
}
