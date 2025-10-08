using Baked.Reporting;

namespace Baked.Test.Reporting;

public class MockingReportResults : TestServiceSpec
{
    [Test]
    public void Mock_report_context_is_provided_during_tests()
    {
        var context = GiveMe.The<IReportContext>();

        var getMock = () => Mock.Get(context);

        getMock.ShouldNotThrow();
    }

    [Test]
    public async Task Sets_up_mock_report_to_return_desired_data()
    {
        MockMe.TheReportContext(data: [[2, "test-1"], [1, "test-2"]]);
        var reportSamples = GiveMe.The<ReportSamples>();

        var result = await reportSamples.GetEntity("test");

        result[0].Count.ShouldBe(2);
        result[0].String.ShouldBe("test-1");
        result[1].Count.ShouldBe(1);
        result[1].String.ShouldBe("test-2");
    }

    [Test]
    public void Sets_up_mock_report_to_throw_query_not_found()
    {
        MockMe.TheReportContext(queryNotFound: true);
        var reportSamples = GiveMe.The<ReportSamples>();

        var action = reportSamples.GetEntity("test");

        action.ShouldThrow<QueryNotFoundException>().Message.ShouldContain("entity");
    }

    [Test]
    public async Task Verifies_execute_with_given_query_and_parameters()
    {
        var reportContext = MockMe.TheReportContext();
        var reportSamples = GiveMe.The<ReportSamples>();

        await reportSamples.GetEntity("test");

        reportContext.VerifyExecute(
            queryName: "entity",
            parameter: ("string", "test%")
        );
    }
}