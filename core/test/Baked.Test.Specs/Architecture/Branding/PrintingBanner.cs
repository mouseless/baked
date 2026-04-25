using Baked.Architecture;
using Baked.Branding;
using Spectre.Console;
using System.Diagnostics;

namespace Baked.Test.Architecture.Branding;

public class PrintingBanner : ArchitectureSpec
{
    IAnsiConsole _realConsole = default!;
    TextWriter _fakeOut = default!;

    public override void SetUp()
    {
        base.SetUp();

        AnsiConsole.Console = AnsiConsole.Create(new()
        {
            Out = new AnsiConsoleOutput(_fakeOut = new StringWriter()),
            Ansi = AnsiSupport.No,
            ColorSystem = ColorSystemSupport.NoColors,
            Interactive = InteractionSupport.No
        });
    }

    public override void TearDown()
    {
        base.TearDown();

        AnsiConsole.Console = _realConsole;
    }

    string ConsoleOutput => _fakeOut?.ToString() ?? string.Empty;

    // Version is shortened to Ver to keep banner width fixed
    static string VersionString =>
        FileVersionInfo.GetVersionInfo(typeof(IBanner).Assembly.Location).FileVersion?[..^2] ??
        throw new("Version not found");

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

        ⢐⠄⣗⡆⡶⡆⡧⡂⣶⡂⣖⡇⣀

         v{{VersionString}} - baked.mouseless.codes - github.com/mouseless/baked


        """);
    }

    [Test]
    public void Baked_banner_does_not_print_on_generate_mode()
    {
        var banner = GiveMe.ABakedBanner(runFlags: RunFlags.Generate);

        banner.Print();

        ConsoleOutput.ShouldBe(string.Empty);
    }
}