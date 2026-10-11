using Baked.Business;

namespace Baked.Test.Business;

[TestFixture("PropertyName", "Property Name")]
[TestFixture("propertyName", "Property Name")]
[TestFixture("property", "Property")]
[TestFixture("invariantName", "Invariant Name")]
[TestFixture("_field.PropertyName", "Property Name")]
[TestFixture("_field.invariantName", "Invariant Name")]
[TestFixture("_field", "")]
[TestFixture("", "")]
[SetUICulture("tr-TR")]
public class LocalizingValidation(string key, string expected)
    : TestSpec()
{
    [Test]
    public void Localization_provides_an_extension_to_provide_localization_function()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate.That(l => throw new(l(key)));

        action.ShouldThrow<Exception>().Message.ShouldBe(expected);
    }

    [Test]
    public void Async_support()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate.ThatAsync(async l =>
        {
            await Task.CompletedTask;

            throw new(l(key));
        });

        action.ShouldThrow<Exception>().Message.ShouldBe(expected);
    }

    [Test]
    public void Async_chain_support()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .ThatAsync(async l =>
            {
                await Task.CompletedTask;

                l(key).ShouldBe(expected);
            })
            .That(l => l(key).ShouldBe(expected))
            .ThatAsync(async l =>
            {
                await Task.CompletedTask;

                throw new(l(key));
            });

        action.ShouldThrow<Exception>().Message.ShouldBe(expected);
    }

    [Test]
    public void Localization_and_service_provider_support()
    {
        var validate = GiveMe.The<Validate>();

        var action = () => validate
            .That((l, sp) =>
            {
                l(key).ShouldBe(expected);
                sp.ShouldNotBeNull();
            })
            .ThatAsync(async (l, sp) =>
            {
                await Task.CompletedTask;

                l(key).ShouldBe(expected);
                sp.ShouldNotBeNull();
            })
            .That((l, sp) =>
            {
                l(key).ShouldBe(expected);
                sp.ShouldNotBeNull();
            })
            .ThatAsync(async (l, sp) =>
            {
                await Task.CompletedTask;

                sp.ShouldNotBeNull();
                throw new(l(key));
            });

        action.ShouldThrow<Exception>().Message.ShouldBe(expected);
    }
}