using Newtonsoft.Json;

namespace Do.Test;

public static class TestServiceNfrExtensions
{
    public async static Task<dynamic?> DeserializeContentToDynamic(this HttpContent content)
    {
        return JsonConvert.DeserializeObject(await content.ReadAsStringAsync());
    }
}
