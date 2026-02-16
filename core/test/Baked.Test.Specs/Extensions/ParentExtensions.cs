using Baked.Playground.Orm;
using Baked.Testing;
using System.Net.Http.Json;

namespace Baked.Test;

public static class ParentExtensions
{
    public static Parent AParent(this Stubber giveMe,
        string? name = default,
        string? surname = default,
        bool withChild = false
    )
    {
        name ??= giveMe.AString();
        surname ??= giveMe.AString();

        var result = giveMe.A<Parent>().With(name, surname);
        if (withChild)
        {
            result.AddChild(giveMe.AString());
        }

        return result;
    }

    public static async Task<dynamic> PostParents(this System.Net.Http.HttpClient client,
        string? name = default,
        string? surname = default
    )
    {
        name ??= "test";
        surname ??= "test";

        var response = await client.PostAsync("/parents", JsonContent.Create(new { name, surname }));
        dynamic? result = await response.Content.Deserialize();

        return result ?? throw new("Response should've been not-null");
    }

    public static async Task PostParentsChildren(this System.Net.Http.HttpClient client, object id)
    {
        var response = await client.PostAsync($"/parents/{id}/children", JsonContent.Create(new { name = "child" }));
        await response.Content.Deserialize();
    }

    public static async Task<dynamic> GetParentsChildren(this System.Net.Http.HttpClient client, object id)
    {
        var response = await client.GetAsync($"/parents/{id}/children");
        dynamic? result = await response.Content.Deserialize();

        return result ?? throw new("Response should've been not-null");
    }
}