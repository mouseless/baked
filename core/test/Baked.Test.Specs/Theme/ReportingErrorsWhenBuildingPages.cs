using Baked.Theme;
using Baked.Ui;
using Baked.Ui.Configuration;

namespace Baked.Test.Theme;

public class ReportingErrorsWhenBuildingPages : TestSpec
{
    [Test]
    public void It_continues_building_pages_even_if_an_exception_occurs()
    {
        var domain = GiveMe.TheDomainModel();
        var pages = new PageDescriptors();
        NewLocaleKey l = key => key;
        var routes = new List<Route> { GiveMe.ARoute(buildFails: true) };

        var action = () => pages.AddPages(routes, domain, l, onComplete: e => { });

        action.ShouldNotThrow();
    }

    [Test]
    public void After_building_pages__it_delegates_reported_error_to_given_error_handler()
    {
        var errors = new List<Exception>();
        var domain = GiveMe.TheDomainModel();
        var pages = new PageDescriptors();
        NewLocaleKey l = key => key;
        var routes = new List<Route> { GiveMe.ARoute(buildFails: true, buildFailMessage: "test") };

        pages.AddPages(routes, domain, l, onComplete: e => errors.AddRange(e.Errors));

        errors.Count.ShouldBe(1);
        errors.ShouldContain(e => e.Message == "test");
    }

    [Test]
    public void It_allows_multiple_errors_in_one_run()
    {
        var errors = new List<Exception>();
        var domain = GiveMe.TheDomainModel();
        var pages = new PageDescriptors();
        NewLocaleKey l = key => key;
        var routes = new List<Route>
        {
            GiveMe.ARoute(buildFails: true, buildFailMessage: "first"),
            GiveMe.ARoute(buildFails: true, buildFailMessage: "second")
        };

        pages.AddPages(routes, domain, l, onComplete: e => errors.AddRange(e.Errors));

        errors.Count.ShouldBe(2);
        errors.ShouldContain(e => e.Message == "first");
        errors.ShouldContain(e => e.Message == "second");
    }
}