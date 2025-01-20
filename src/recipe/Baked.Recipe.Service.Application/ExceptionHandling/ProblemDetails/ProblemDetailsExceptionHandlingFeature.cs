using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ProblemDetailsExceptionHandlingFeature(Setting<string>? _typeUrlFormat = default)
    : IFeature<ExceptionHandlingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault("""
            {
              "Logging": {
                "LogLevel": {
                  "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware": "None"
                }
              }
            }
            """);
        });

        configurator.ConfigureDomainTypeCollection(types =>
        {
            types.Add<HandledException>();
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            var domain = configurator.Context.GetDomainModel();

            var domainModel = configurator.Context.GetDomainModel();
            var exceptionHandlerTypes = domainModel.Types.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo<IExceptionHandler>());

            generatedAssemblies.Add(nameof(ProblemDetailsExceptionHandlingFeature),
                assembly =>
                {
                    assembly
                        .AddReferenceFrom<ProblemDetailsExceptionHandlingFeature>()
                        .AddCodes(new ExceptionHandlerAdderTemplate(exceptionHandlerTypes));

                    foreach (var entity in exceptionHandlerTypes)
                    {
                        entity.Apply(t => assembly.AddReferenceFrom(t));
                    }
                },
                compilationOptions => compilationOptions.WithUsings(
                    "Baked",
                    "Baked.ExceptionHandling",
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection",
                    "System",
                    "System.Linq",
                    "System.Collections",
                    "System.Collections.Generic",
                    "System.Threading.Tasks"
                )
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddFromAssembly(configurator.Context.GetGeneratedAssembly(nameof(ProblemDetailsExceptionHandlingFeature)));
            services.AddSingleton<IExceptionHandler, AuthenticationExceptionHandler>();
            services.AddSingleton<IExceptionHandler, UnauthorizedAccessExceptionHandler>();
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
            services.AddSingleton(new ExceptionHandlerSettings(_typeUrlFormat));
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    app.UseMiddleware<ExceptionLoggerMiddleware>();
                    app.UseExceptionHandler();
                    app.UseStatusCodePages();
                },
                order: -10
            );
        });
    }
}

public class ExceptionHandlerAdderTemplate(IEnumerable<TypeModel> _types) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace AutoMapFeature;

        public class SingletonServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_types, exceptionHandler => $$"""
                services.AddSingleton<IExceptionHandler,{{exceptionHandler.CSharpFriendlyFullName}}>(forward: true);
            """)}}
            }
        }
    """;
}