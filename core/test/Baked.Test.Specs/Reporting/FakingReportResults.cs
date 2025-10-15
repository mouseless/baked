using Baked.Reporting;

namespace Baked.Test.Reporting;

/// <note>
/// Fake data is in `Baked.Test/Reporting/Fake/entity.json`.
/// Look the data in this json file to understand why fake report context
/// returns below expected results.
/// </note>
public class FakingReportResults : TestSpec
{
    [Test]
    public async Task Loads_fake_data_from_resource_using_configured_base_path()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", "test" } });

        result.Length.ShouldBePositive();
    }

    [Test]
    public void When_report_was_not_found_throws_query_not_found_exception()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var action = context.Execute("non-existing", []);

        action.ShouldThrow<QueryNotFoundException>().Message.ShouldContain("non-existing");
    }

    [Test]
    public async Task Clears_keys_from_rows__returning_just_cells()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", "test" } });

        result[0][0].ShouldDeeplyBe(2);
        result[0][1].ShouldDeeplyBe("test 1");
        result[1][0].ShouldDeeplyBe(1);
        result[1][1].ShouldDeeplyBe("test 2");
    }

    [Test]
    public async Task Uses_argument_values_to_find_different_fake_data_for_the_same_report()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", "filtered" } });

        result[0][0].ShouldDeeplyBe(4);
        result[0][1].ShouldDeeplyBe("filtered 1");
        result[1][0].ShouldDeeplyBe(3);
        result[1][1].ShouldDeeplyBe("filtered 2");
    }

    [Test]
    public async Task Argument_matchers_uses_regex_patterns_to_allow_wildcard_like_expressions()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", "reg" } });

        result[0][0].ShouldDeeplyBe(6);
        result[0][1].ShouldDeeplyBe("reg-x");
        result[1][0].ShouldDeeplyBe(5);
        result[1][1].ShouldDeeplyBe("reg-y");
    }

    [Test]
    public async Task Parameter_value_can_be_null()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", null } });

        result[0][0].ShouldDeeplyBe(7);
        result[0][1].ShouldBeNull();
    }

    [Test]
    public async Task When_no_data_matches_argument_returns_empty_array()
    {
        var context = GiveMe.AFakeReportContext(basePath: "Reporting/Fake");

        var result = await context.Execute("entity", new() { { "string", "non-existing" } });

        result.ShouldBeEmpty();
    }
}