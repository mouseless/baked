namespace Baked.Test;

public class ReportingDiagnostics
{
    [Test]
    public void Error_comes_in_maroon_along_with_a_link()
    {
        var messages = new List<DiagnosticMessage>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => throw new("test"));
        }

        messages[0].ToString().ShouldBe(
            "[bold maroon]error [link=https://baked.mouseless.codes/errors#unknown]B9999[/][/]: test"
        );
    }

    [Test]
    public void Non_diagnostics_exceptions_are_reported_along_with_their_stack_trace()
    {
        var messages = new List<DiagnosticMessage>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => throw new(string.Empty));
        }

        messages.Count.ShouldBe(2);
        messages[1].Level.ShouldBe("info");
        messages[1].Message.ShouldContain($"{nameof(ReportingDiagnostics)}.cs:line");
    }

    [Test]
    public void Warning_comes_in_darkorange3_along_with_a_link()
    {
        var messages = new List<DiagnosticMessage>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => Diagnostics.ReportWarning(DiagnosticCode.Unknown, "test"));
        }

        messages[0].ToString().ShouldBe(
            "[bold darkorange3]warning [link=https://baked.mouseless.codes/errors#unknown]B9999[/][/]: test"
        );
    }

    [Test]
    public void Info_comes_in_cyan()
    {
        var messages = new List<DiagnosticMessage>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => Diagnostics.ReportInfo("test"));
        }

        messages[0].ToString().ShouldBe(
            "[bold cyan]info[/]: test"
        );
    }
}