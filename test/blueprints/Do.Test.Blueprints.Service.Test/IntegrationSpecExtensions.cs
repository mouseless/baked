using System.Net.Http.Json;
using System.Text.Json;

namespace Do.Test;

public static class IntegrationSpecExtensions
{
    public static async Task<List<JsonElement>> GetEntities(this HttpClient client)
    {
        var response = await client.GetAsync("entities");

        return await response.Content.ReadFromJsonAsync<List<JsonElement>>() ?? throw new("No entities in database");
    }
}
