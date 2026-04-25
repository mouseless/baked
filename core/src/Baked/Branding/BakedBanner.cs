using Baked.Architecture;
using Spectre.Console;

namespace Baked.Branding;

public sealed class BakedBanner(RunFlags _runFlags)
    : IBanner
{
    public void Print()
    {
        if (_runFlags == RunFlags.Generate) { return; }

        var assembly = GetType().Assembly;
        var version = assembly.GetName().Version ?? new(0, 0, 0);
        var versionString = $"{version.Major}.{version.Minor}.{version.Build}";

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold][red]⢐⠄[/]⣗⡆⡶⡆⡧⡂⣶⡂⣖⡇[red]⣀[/][/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[gray] v{versionString} - [link=https://baked.mouseless.codes]docs[/] - [link=https://github.com/mouseless/baked]source[/][/]");
        AnsiConsole.WriteLine();
    }
}