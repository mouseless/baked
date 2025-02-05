using Baked.Architecture;
using Baked.HttpServer;
using Baked.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Security.Claims;
using System.Text;

namespace Baked;

public static class HttpServerExtensions
{
    public static void AddHttpServer(this List<ILayer> layers) =>
        layers.Add(new HttpServerLayer());

    public static WebApplicationBuilder GetWebApplicationBuilder(this ApplicationContext context) =>
        context.Get<WebApplicationBuilder>();

    public static ConfigurationManager GetConfigurationManager(this ApplicationContext context) =>
        context.Get<ConfigurationManager>();

    public static WebApplication GetWebApplication(this ApplicationContext context) =>
        context.Get<WebApplication>();

    public static void ConfigureAuthenticationCollection(this LayerConfigurator configurator, Action<IAuthenticationCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureMiddlewareCollection(this LayerConfigurator configurator, Action<IMiddlewareCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureEndpointRouteBuilder(this LayerConfigurator configurator, Action<IEndpointRouteBuilder> configuration) =>
        configurator.Configure(configuration);

    public static void Add(this IAuthenticationCollection authentications, string scheme, Action<AuthenticationBuilder> useBuilder,
       Func<HttpContext, bool>? handles = default
    ) => authentications.Add(new(scheme, useBuilder, handles ?? (_ => true)));

    public static void Add<T>(this IMiddlewareCollection middlewares,
        int order = default
    ) => middlewares.Add(new(app => app.UseMiddleware<T>(), order));

    public static void Add(this IMiddlewareCollection middlewares, Action<IApplicationBuilder> configure,
        int order = default
    ) => middlewares.Add(new(configure, order));

    public static HttpRequest AnHttpRequest(this Stubber giveMe,
        string? schema = default,
        string? host = default,
        string? path = default,
        string? method = default,
        Dictionary<string, string>? query = default,
        Dictionary<string, string>? header = default,
        Dictionary<string, string>? form = default,
        object? body = default,
        object[]? metadata = default,
        ClaimsPrincipal? user = default
    )
    {
        schema ??= "http";
        host ??= "test.com";
        path ??= string.Empty;
        method ??= form is null && body is null ? "GET" : "POST";

        var result = giveMe.AnHttpContext(metadata: metadata, user: user).Request;

        result.Scheme = schema;
        result.Host = new(host);
        result.Path = path;
        result.Method = method;

        if (query is not null)
        {
            result.QueryString = new($"?{query.ToQueryString()}");
        }

        if (header is not null)
        {
            foreach (var (key, value) in header)
            {
                result.Headers.Append(key, value);
            }
        }

        if (form is not null)
        {
            result.ContentType = "application/x-www-form-urlencoded";
            result.Form = new FormCollection(form.ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value)));
        }

        if (body is not null)
        {
            result.ContentType = "application/json";
            result.Body = new MemoryStream(Encoding.UTF8.GetBytes(body.ToJsonString()));
        }

        return result;
    }

    public static HttpContext AnHttpContext(this Stubber giveMe,
        object[]? metadata = default,
        ClaimsPrincipal? user = default
    )
    {
        var features = new FeatureCollection();
        features.Set(giveMe.Spec.MockMe.AnEndpointFeature(metadata: metadata));
        features.Set<IHttpRequestFeature>(new HttpRequestFeature());
        features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature { User = user });

        return new DefaultHttpContext(features);
    }

    public static IEndpointFeature AnEndpointFeature(this Mocker mockMe,
        object[]? metadata = default
    )
    {
        metadata ??= [];

        var mock = new Mock<IEndpointFeature>();
        var endpoint = new Endpoint(mockMe.Spec.GiveMe.ARequestDelegate(), new(metadata), mockMe.Spec.GiveMe.AString());

        mock.Setup(ef => ef.Endpoint).Returns(endpoint);

        return mock.Object;
    }

    public static RequestDelegate ARequestDelegate(this Stubber _) =>
        _ => Task.CompletedTask;
}