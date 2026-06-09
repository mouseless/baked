using Baked.Buildtime.Diagnostics;

namespace Baked.Test.Buildtime;

public class ReportingDiagnostics
{
    [Test]
    public void Error_comes_in_a_red_tone_along_with_a_link()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => throw new("test"));
        }

        messages[0].ToString().ShouldContain(
            "[bold maroon]error [link=https://baked.mouseless.codes/errors#unknown]B9999[/][/]: test"
        );
    }

    [Test]
    public void Diagnostic_exceptions_includes_code_and_line_number()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => throw DiagnosticCode.UndefinedLevel.Exception("test"));
        }

        messages[0].ToString().ShouldContain(
            "[gray]«[/] $\"[magenta]ReportingDiagnostics:27[/]"
        );
    }

    [Test]
    public void Non_diagnostics_exceptions_are_reported_along_with_their_stack_trace()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => throw new(string.Empty));
        }

        messages.Count.ShouldBe(2);
        messages[1].Level.ShouldBe("info");
        messages[1].Message.ShouldContain($"{nameof(ReportingDiagnostics)}.cs:line");
    }

    [Test]
    public void Warning_comes_in_an_orange_tone_along_with_a_link()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => diagnostics.ReportWarning(DiagnosticCode.Unknown, "test"));
        }

        messages[0].ToString().ShouldContain(
            "[bold darkorange3]warning [link=https://baked.mouseless.codes/errors#unknown]B9999[/][/]: test"
        );
    }

    [Test]
    public void Warning_includes_code_and_line_number()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => diagnostics.ReportWarning(DiagnosticCode.Unknown, "test"));
        }

        messages[0].ToString().ShouldContain(
            "[gray]«[/] $\"[magenta]ReportingDiagnostics:69[/]"
        );
    }

    [Test]
    public void Info_comes_in_a_blue_tone()
    {
        var messages = new List<DiagnosticMessage>();
        using (var diagnostics = Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            diagnostics.Diagnose(() => diagnostics.ReportInfo("test"));
        }

        messages[0].ToString().ShouldBe(
            "[bold cyan]info[/]: test"
        );
    }
}