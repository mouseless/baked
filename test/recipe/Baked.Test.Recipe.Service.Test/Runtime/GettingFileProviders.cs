using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Runtime;

public class GettingFileProviders : TestServiceSpec
{
    [Test]
    public void Returns_composite_file_provider_when_only_one_provider_is_requested()
    {
        var provider = GiveMe.TheServiceProvider().GetRequiredService<IFileProvider>();

        provider.GetType().ShouldBe(typeof(CompositeFileProvider));
    }

    [Test]
    public void Returns_all_file_providers_when_all_providers_are_requested()
    {
        var providers = GiveMe.TheServiceProvider().GetServices<IFileProvider>();

        providers.Any(p => !p.GetType().IsAssignableFrom(typeof(CompositeFileProvider))).ShouldBeTrue();
    }
}
