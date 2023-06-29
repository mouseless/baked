namespace Do.Test.Blueprints.Service.Greeting;

public static class HttpLayerExtensions
{
    public static void ConfigureApplicationBuilder(this object target, Action<IApplicationBuilder> configure)
    {
        if (target is IApplicationBuilder app)
        {
            configure(app);
        }
    }

    public static void ConfigureEndpointRouteBuilder(this object target, Action<IEndpointRouteBuilder> configure)
    {
        // this should not just be a cast. target should not be an object
        // here target may not be ready to provide route even if it is (or has) an IEndpointRouteBuilder
        if (target is IEndpointRouteBuilder route)
        {
            configure(route);
        }
    }
}
