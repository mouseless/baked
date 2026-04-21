namespace Baked.Test;

public class ReportingDiagnostics
{
    [Test]
    public void Non_diagnostics_exceptions_are_reported_along_with_their_stack_trace()
    {
        var messages = new List<string>();
        using (Diagnostics.Start("test", result => messages.AddRange(result.Messages)))
        {
            Diagnostics.Diagnose(() => throw new("test"));
        }

        messages.Count.ShouldBe(2);
        messages[0].ShouldBe("error B9999: test (See: https://baked.mouseless.codes/errors#fatal)");
        messages[1].ShouldStartWith("info:");
        messages[1].ShouldContain("ReportingDiagnostics.cs:line 11");
    }
}