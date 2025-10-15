using Newtonsoft.Json;

namespace Baked.Test;

public static class TestNfrExtensions
{
    public async static Task<object?> Deserialize(this HttpContent content)
    {
        return JsonConvert.DeserializeObject(await content.ReadAsStringAsync());
    }
}