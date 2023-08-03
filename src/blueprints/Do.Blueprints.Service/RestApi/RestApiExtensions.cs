using Do.Architecture;
using Do.RestApi;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Do;

public static class RestApiExtensions
{
    public static void AddRestApi(this IList<ILayer> source) => source.Add(new RestApiLayer());

    public static void ConfigureSwaggerGenOptions(this LayerConfigurator source, Action<SwaggerGenOptions> configuration) => source.Configure(configuration);
    public static void ConfigureSwaggerOptions(this LayerConfigurator source, Action<SwaggerOptions> configuration) => source.Configure(configuration);
    public static void ConfigureSwaggerUIOptions(this LayerConfigurator source, Action<SwaggerUIOptions> configuration) => source.Configure(configuration);
}
