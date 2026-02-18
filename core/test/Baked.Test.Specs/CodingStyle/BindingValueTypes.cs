using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingValueTypes : TestNfr
{
    string? Item(int index, bool @null) =>
        @null ? null : $"item{index}";

    [TestCase(false, false)]
    [TestCase(true, false)]
    [TestCase(true, true)]
    public async Task InBody(bool nullable, bool @null)
    {
        var response = await Client.PostAsync($"/{(nullable ? "nullable" : "method")}-samples/value-type-parameters",
            JsonContent.Create(
                new
                {
                    single = Item(1, @null),
                    enumerable = new[] { Item(2, @null), Item(3, @null) },
                    array = new[] { Item(4, @null), Item(5, @null) }
                }
            )
        );

        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        ((string?)actual?[0]).ShouldBe(Item(1, @null));
        ((string?)actual?[1]).ShouldBe(Item(2, @null));
        ((string?)actual?[2]).ShouldBe(Item(3, @null));
        ((string?)actual?[3]).ShouldBe(Item(4, @null));
        ((string?)actual?[4]).ShouldBe(Item(5, @null));
    }

    [TestCase(false, false)]
    [TestCase(true, false)]
    [TestCase(true, true)]
    public async Task InQuery(bool nullable, bool @null)
    {
        var response = await Client.GetAsync($"/{(nullable ? "nullable" : "method")}-samples/value-type-parameters" +
            $"?single={Item(1, @null)}" +
            $"&enumerable={Item(2, @null)}&enumerable={Item(3, @null)}" +
            $"&array={Item(4, @null)}&array={Item(5, @null)}"
        );

        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        ((string?)actual?[0]).ShouldBe(Item(1, @null));
        ((string?)actual?[1]).ShouldBe(Item(2, @null));
        ((string?)actual?[2]).ShouldBe(Item(3, @null));
        ((string?)actual?[3]).ShouldBe(Item(4, @null));
        ((string?)actual?[4]).ShouldBe(Item(5, @null));
    }

    [TestCase(false, false)]
    [TestCase(true, false)]
    [TestCase(true, true)]
    public async Task InRecord(bool nullable, bool @null)
    {
        var response = await Client.PostAsync($"/{(nullable ? "nullable" : "method")}-samples/record-with-value-type",
            JsonContent.Create(
                new
                {
                    record = new
                    {
                        single = Item(1, @null),
                        enumerable = new[] { Item(2, @null), Item(3, @null) },
                        array = new[] { Item(4, @null), Item(5, @null) }
                    }
                }
            )
        );

        dynamic? actual = await response.Content.Deserialize();

        ((int?)actual?.Count).ShouldBe(5);
        ((string?)actual?[0]).ShouldBe(Item(1, @null));
        ((string?)actual?[1]).ShouldBe(Item(2, @null));
        ((string?)actual?[2]).ShouldBe(Item(3, @null));
        ((string?)actual?[3]).ShouldBe(Item(4, @null));
        ((string?)actual?[4]).ShouldBe(Item(5, @null));
    }
}