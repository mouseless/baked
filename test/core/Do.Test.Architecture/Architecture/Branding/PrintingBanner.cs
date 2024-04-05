using Do.Branding;
using System.Diagnostics;

namespace Do.Test.Architecture.Branding;

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
        var forge = GiveMe.AForge(banner: banner);

        forge.Application(_ => { });

        banner.VerifyPrinted();
    }

    [Test]
    public void Do_banner_prints_do_logo__version_and_project_links_to_console()
    {
        var banner = GiveMe.ADoBanner();

        banner.Print();

        ConsoleOutput.ShouldMatch($$"""

         ▀▄   █▀▀▀▄ █▀▀▀█
          ▄▀  █   █ █   █
         ▀    ▀▀▀▀  ▀▀▀▀▀ ▀▀▀▀▀

        Version: v{{VersionString}}
        Docs: https://do.mouseless.codes
        Source: https://github.com/mouseless/do

        """);
    }
}
