namespace Do.Test;

public class IntegrationSpecRunner
{
    public static void Run(string[] args)
    {
        var type = Type.GetType(GetArgumentValue(args[0], '=')) ?? throw new("Type should have existed");
        var method = typeof(IntegrationSpec<>).MakeGenericType(type).GetMethod(GetArgumentValue(args[1], '=')) ?? throw new("Method should have existed");

        method.Invoke(Activator.CreateInstance(type), null);
    }

    static string GetArgumentValue(string arg, char index) =>
        arg[(arg.IndexOf(index) + 1)..];
}
