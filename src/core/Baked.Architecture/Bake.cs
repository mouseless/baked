using Baked.Architecture;
using Baked.Branding;
using System.Globalization;

namespace Baked;

public class Bake(IBanner _banner, Func<Application> _newApplication)
{
    public static Bake New => new(new BakedBanner(), () => new(new()));

    public Application Application(Action<ApplicationDescriptor> describe)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        _banner.Print();
        var descriptor = new ApplicationDescriptor();

        describe(descriptor);

        return _newApplication().With(descriptor);
    }
}