using Baked.Architecture;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.HttpServer.HttpServerLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.RestApi;

public class RestApiLayer : LayerBase<GenerateCode, AddServices, Build>
{
    public const int MinConventionOrder = -ConventionOrderLimit;
    public const int MaxConventionOrder = ConventionOrderLimit;

    readonly ApiModel _apiModel = new();
    readonly IApplicationPartCollection _applicationParts = new ApplicationPartCollection();
    readonly MvcNewtonsoftJsonOptions _mvcNewtonsoftJsonOptions = [];
    readonly SwaggerGenOptions _swaggerGenOptions = new();
    readonly SwaggerOptions _swaggerOptions = new();
    readonly SwaggerUIOptions _swaggerUIOptions = new();

    public RestApiLayer()
    {
        _mvcNewtonsoftJsonOptions.SerializerSettings.ContractResolver = new ExtendedContractResolver();
    }

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        var generatedAssemblies = Context.GetGeneratedAssemblyCollection();
        _apiModel.References.Add<RestApiLayer>();
        _apiModel.References.Add<ApiControllerAttribute>();
        _apiModel.Usings.AddRange(
        [
            "Microsoft.AspNetCore.Mvc",
            "System",
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic",
            "System.Threading.Tasks"
        ]);

        return phase.CreateContextBuilder()
            .Add(_apiModel)
            .OnDispose(() =>
            {
                generatedAssemblies.Add(nameof(RestApiLayer),
                    assembly => assembly
                        .AddReferences(_apiModel.References)
                        .AddCodes(new ApiCodeTemplate(_apiModel)),
                    compilerOptions => compilerOptions.WithUsings(_apiModel.Usings)
                );
            })
            .Build();
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var controllerAssembly = Context.GetGeneratedAssembly(nameof(RestApiLayer));
        var services = Context.GetServiceCollection();

        services.AddMvcCore().AddApiExplorer();
        services.AddSwaggerGen();

        _swaggerGenOptions.DocInclusionPredicate((name, api) => true);
        _swaggerGenOptions.TagActionsBy(api => [api.GroupName]);

        return phase.CreateContextBuilder()
            .Add(_applicationParts)
            .Add(_mvcNewtonsoftJsonOptions)
            .Add(_swaggerGenOptions)
            .OnDispose(() =>
            {
                services.AddControllers()
                    .AddNewtonsoftJson(_mvcNewtonsoftJsonOptions)
                    .AddApplicationPart(controllerAssembly)
                    .AddApplicationParts(_applicationParts);
                services.ConfigureSwaggerGen(config =>
                {
                    config.SwaggerGeneratorOptions = _swaggerGenOptions.SwaggerGeneratorOptions;
                    config.SchemaGeneratorOptions = _swaggerGenOptions.SchemaGeneratorOptions;
                    config.ParameterFilterDescriptors = _swaggerGenOptions.ParameterFilterDescriptors;
                    config.RequestBodyFilterDescriptors = _swaggerGenOptions.RequestBodyFilterDescriptors;
                    config.OperationFilterDescriptors = _swaggerGenOptions.OperationFilterDescriptors;
                    config.DocumentFilterDescriptors = _swaggerGenOptions.DocumentFilterDescriptors;
                    config.SchemaFilterDescriptors = _swaggerGenOptions.SchemaFilterDescriptors;
                });
            })
            .Build();
    }

    protected override PhaseContext GetContext(Build phase)
    {
        var app = Context.GetWebApplication();

        app.UseRouting();
        app.MapControllers();

        return phase.CreateContextBuilder()
            .Add(_swaggerOptions)
            .Add(_swaggerUIOptions)
            .OnDispose(() =>
                app
                    .UseSwagger(config =>
                    {
                        config.OpenApiVersion = _swaggerOptions.OpenApiVersion;
                        config.PreSerializeFilters.AddRange(_swaggerOptions.PreSerializeFilters);
                        config.RouteTemplate = _swaggerOptions.RouteTemplate;
                    })
                    .UseSwaggerUI(config =>
                    {
                        config.ConfigObject = _swaggerUIOptions.ConfigObject;
                        config.DocumentTitle = _swaggerUIOptions.DocumentTitle;
                        config.HeadContent = _swaggerUIOptions.HeadContent;
                        config.IndexStream = _swaggerUIOptions.IndexStream;
                        config.Interceptors = _swaggerUIOptions.Interceptors;
                        config.OAuthConfigObject = _swaggerUIOptions.OAuthConfigObject;
                        config.RoutePrefix = _swaggerUIOptions.RoutePrefix;
                    })
            )
            .Build();
    }
}