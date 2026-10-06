using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingEnums : TestNfr
{
    [Test]
    public async Task Normal()
    {
        var response = await Client.PostAsync("/entities", JsonContent.Create(
            new
            {
                @enum = "member1"
            }
        ));
        dynamic? entity = await response.Content.Deserialize();
        object? @enum = entity?.@enum;

        @enum.ShouldDeeplyBe("member1");
    }

    [Test]
    public async Task Flags()
    {
        var response = await Client.PostAsync("/entities", JsonContent.Create(
            new
            {
                flagsEnum = new[] { "flag1", "flag3" }
            }
        ));
        dynamic? entity = await response.Content.Deserialize();
        object? flagsEnum = entity?.flagsEnum;

        flagsEnum.ShouldDeeplyBe(
            new[] { "flag1", "flag3" }
        );
    }
}