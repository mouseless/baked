using Baked.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Test.Business;

public class Validation : TestSpec
{
    [Test]
    public void Validate_helper_contains_that_method_to_be_used_by_extensions()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .That(() => throw new("test"))
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }

    [Test]
    public void Validation_supports_chaining()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .That(() => { })
            .That(() => throw new("test"))
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }

    [Test]
    public void Validation_supports_async()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .ThatAsync(async () => await Task.CompletedTask)
        ;

        action.ShouldNotThrow();
    }

    [Test]
    public void It_allows_async_and_sync_validations_together()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .That(() => { })
            .ThatAsync(async () => await Task.CompletedTask)
            .That(() => throw new("test"))
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }

    [Test]
    public void Optionally_it_provides_service_provider_for_complex_validations()
    {
        MockMe.TheTime(now: new(2026, 10, 6, 17, 18, 00));
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .That(sp =>
            {
                var tp = sp.GetRequiredService<TimeProvider>();

                if (tp.GetNow() != new DateTime(2026, 10, 6, 17, 18, 00)) { return; }

                throw new("test");
            })
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }

    [Test]
    public void Optionally_it_provides_service_provider_for_complex_validations__async()
    {
        MockMe.TheTime(now: new(2026, 10, 6, 17, 18, 00));
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .ThatAsync(async sp =>
            {
                await Task.CompletedTask;

                var tp = sp.GetRequiredService<TimeProvider>();
                if (tp.GetNow() != new DateTime(2026, 10, 6, 17, 18, 00)) { return; }

                throw new("test");
            })
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }

    [Test]
    public void Provide_sp_in_async_chains()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .ThatAsync(async () => await Task.CompletedTask)
            .That(sp => sp.GetRequiredService<TimeProvider>())
            .ThatAsync(async sp =>
            {
                await Task.CompletedTask;

                sp.GetRequiredService<TimeProvider>();
            })
            .That(() => throw new("test"))
        ;

        action.ShouldThrow<Exception>().Message.ShouldBe("test");
    }
}