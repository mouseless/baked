namespace Baked.Test.Business;

public class IncludingTypeForPolymorphicData : TestNfr
{
    [Test]
    public async Task Polymorphic_data()
    {
        var response = await Client.GetAsync("/method-samples/data");
        object? actual = await response.Content.Deserialize();

        actual?.ShouldDeeplyBe(new
        {
            numeric = 42,
            text = "data",
            polymorphic = new Dictionary<string, object>()
            {
                ["$type"] = "implemented",
                ["name"] = "data"
            },
            calculatedText = "Calculated data"
        });
    }
}