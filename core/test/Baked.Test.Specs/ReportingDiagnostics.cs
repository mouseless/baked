namespace Baked.Test;

public class ReportingDiagnostics
{
    [Test]
    public void Non_diagnostics_exceptions_are_reported_along_with_their_stack_trace()
    {
        var messages = new List<DiagnosticMessage>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => throw new("test"));
        }

        messages.Count.ShouldBe(2);
        messages[0].ToString().ShouldBe(
            "\x1b[1m\x1b[31merror \x1b]8;;https://baked.mouseless.codes/errors#fatal\x1b\\B9999\x1b]8;;\x1b\\\x1b[0m: test"
        );
        messages[1].Level.ShouldBe("info");
        messages[1].Message.ShouldContain("ReportingDiagnostics.cs:line 11");
    }
}