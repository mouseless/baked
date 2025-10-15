using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Core;

public class ReadingResourcesDuringTests : TestSpec
{
    [TestCase("Core/DomainEmbedded.txt")]
    public void Specs_includes_business_resources(string subpath)
    {
        var provider = GiveMe.The<IFileProvider>();

        var exists = provider.Exists(subpath);

        exists.ShouldBeTrue();
    }

    [TestCase("Core/ApplicationEmbedded.txt")]
    [TestCase("Core/ApplicationPhysical.txt")]
    public void Specs_excludes_application_resources(string subpath)
    {
        var provider = GiveMe.The<IFileProvider>();

        var exists = provider.Exists(subpath);

        exists.ShouldBeFalse();
    }
}