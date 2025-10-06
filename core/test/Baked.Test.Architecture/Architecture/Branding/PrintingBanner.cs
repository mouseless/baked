using Baked.Branding;
using System.Diagnostics;

namespace Baked.Test.Architecture.Branding;

public class PrintingBanner : ArchitectureSpec
{
    TextWriter _realOut = default!;
    TextWriter _fakeOut = default!;

    public override void SetUp()
    {
        base.SetUp();

        _realOut = Console.Out;
        Console.SetOut(_fakeOut = new StringWriter());
    }

    public override void TearDown()
    {
        base.TearDown();

        Console.SetOut(_realOut);
    }

    string ConsoleOutput => _fakeOut?.ToString() ?? string.Empty;

    // Version is shortened to Ver to keep banner width fixed
    string VersionString => FileVersionInfo.GetVersionInfo(typeof(IBanner).Assembly.Location).FileVersion?[..^2] ?? throw new("Version not found");

    [Test]
    public void It_prints_banner_prior_to_build()
    {
        var banner = MockMe.ABanner();
        var bake = GiveMe.ABake(banner: banner);

        bake.Application(_ => { });

        banner.VerifyPrinted();
    }

    [Test]
    public void Baked_banner_prints_baked_logo__version_and_project_links_to_console()
    {
        var banner = GiveMe.ABakedBanner();

        banner.Print();

        ConsoleOutput.ShouldContainWithoutWhitespace($$"""

        ▄  █▄▄ ▄▄▄ █ ▄ ▄▄▄ ▄▄█
        ▄▀ █▄█ █▀█ █▀▄ ██▄ █▄█ ▄▄

        version: v{{VersionString}}
        docs: https://baked.mouseless.codes
        source: https://github.com/mouseless/baked

        """);
    }
}