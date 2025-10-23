using Baked.Architecture;

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

        var brand = ConsoleColor.DarkRed;
        var white = ConsoleColor.White;
        var foreground = ConsoleColor.DarkGray;

        L();
        W("▄  ", brand).L("█▄▄ ▄▄▄ █ ▄ ▄▄▄ ▄▄█", white);
        W("▄▀ ", brand).W("█▄█ █▀█ █▀▄ ██▄ █▄█", white).L(" ▄▄", brand);
        L();
        L($"version: v{versionString}", foreground);
        L("docs: https://baked.mouseless.codes", foreground);
        L("source: https://github.com/mouseless/baked", foreground);
        L();
    }

    BakedBanner L(string? message = default, ConsoleColor? color = default) => W($"{message ?? string.Empty}{Environment.NewLine}", color);
    BakedBanner W(string message, ConsoleColor? color = default)
    {
        color ??= Console.ForegroundColor;

        var old = Console.ForegroundColor;

        Console.ForegroundColor = color.Value;
        Console.Write(message);

        Console.ForegroundColor = old;

        return this;
    }
}