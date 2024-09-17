using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text;

namespace Baked.Configuration;

public class JsonConfigurationSource(string _json)
    : StreamConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new JsonStreamConfigurationProvider(new() { Stream = new MemoryStream(Encoding.UTF8.GetBytes(_json)) });
}