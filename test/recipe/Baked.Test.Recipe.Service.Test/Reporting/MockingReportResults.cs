namespace Baked.Test.Reporting;

public class MockingReportResults : TestServiceSpec
{
    [Test]
    [Ignore("not implemented")]
    public async Task Will_do() =>
      await this.ShouldFailAsync();
}