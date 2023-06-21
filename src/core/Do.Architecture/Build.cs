using Do.Architecture;

namespace Do;

public class Build
{
    public static Build Application => new();

    public IRunnable As(Action<object> build)
    {
        Banner.Print();

        var result = new Application();

        build(result);

        return result;
    }
}
