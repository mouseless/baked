using Do.Architecture;
using Do.Branding;

namespace Do;

public class Build
{
    public static Build Application => new(new DoBanner());

    readonly IBanner _banner;

    public Build(IBanner banner) => _banner = banner;

    public IRunnable As(Action<object> build)
    {
        _banner.Print();

        var result = new Application();

        build(result);

        return result;
    }
}
