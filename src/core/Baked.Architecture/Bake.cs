using Baked.Architecture;
using Baked.Branding;
using System.Globalization;

namespace Baked;

public class Bake(IBanner _banner, Func<Application> _newApplication,
    bool _bake = false,
    bool _start = true
)
{
    public static Bake New => new(new BakedBanner(), () => new(new()),
        _bake: Environment.GetCommandLineArgs().Any(a => a.EndsWith("bake")),
        _start: !Environment.GetCommandLineArgs().Any(a => a.EndsWith("no-start"))
    );

    public Application Application(Action<ApplicationDescriptor> describe)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        _banner.Print();
        var descriptor = new ApplicationDescriptor();

        describe(descriptor);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Nfr")
        {
            return _newApplication().With(descriptor, true, true);
        }

        return _newApplication().With(descriptor, _bake, _start);
    }
}