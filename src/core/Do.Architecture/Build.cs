using Do.Architecture;
using Do.Branding;

namespace Do;

public class Build
{
    public static Build Application => new(new DoBanner(), () => new(new()));

    readonly IBanner _banner;
    readonly Func<Application> _newApplication;

    public Build(IBanner banner, Func<Application> newApplication)
    {
        _banner = banner;
        _newApplication = newApplication;
    }

    public IRunnable As(Action<Application> build)
    {
        _banner.Print();

        var result = _newApplication();

        build(result);

        return result;
    }
}
