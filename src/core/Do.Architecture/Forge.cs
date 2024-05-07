using Do.Architecture;
using Do.Branding;
using System.Globalization;

namespace Do;

public class Forge(IBanner _banner, Func<Application> _newApplication)
{
    public static Forge New => new(new DoBanner(), () => new(new()));

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