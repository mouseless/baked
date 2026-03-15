using Newtonsoft.Json;

namespace Baked.Test;

public static class TestNfrExtensions
{
    extension(HttpContent content)
    {
        public async Task<object?> Deserialize()
        {
            return JsonConvert.DeserializeObject(await content.ReadAsStringAsync());
        }
    }
}