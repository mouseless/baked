using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Theme.Default.Components;

using ReportPageC = Baked.Ui.Component.ReportPage;

namespace Baked.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddSingleById<Entities>();
            builder.Conventions.AddSingleById<Parents>();
            builder.Conventions.AddSingleById<Children>();
            builder.Conventions.AddConfigureAction<AuthenticationSamples>(nameof(AuthenticationSamples.FormPostAuthenticate), useForm: true);
            builder.Conventions.AddConfigureAction<DocumentationSamples>(nameof(DocumentationSamples.Route), parameter: p =>
            {
                p["route"].From = ParameterModelFrom.Route;
                p["route"].RoutePosition = 2;
            });
            builder.Conventions.AddConfigureAction<ExceptionSamples>(nameof(ExceptionSamples.Throw), parameter: p => p["handled"].From = ParameterModelFrom.Query);

            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.UpdateRoute),
                routeParts: ["override-samples", "override", "update-route"],
                method: HttpMethod.Post
            );
            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.Parameter),
                parameter: parameter =>
                {
                    parameter["parameter"].Name = "id";
                    parameter["parameter"].From = ParameterModelFrom.Route;
                    parameter["parameter"].RoutePosition = 2;
                }
            );
            builder.Conventions.AddOverrideAction<OverrideSamples>(nameof(OverrideSamples.RequestClass),
                useRequestClassForBody: false
            );

            builder.Conventions.SetTypeMetadata(
                attribute: _ => new CustomAttribute(),
                when: c => c.Type.Is<Class>()
            );
            builder.Conventions.AddTypeMetadataConfiguration<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) => c.Type.Is<Class>()
            );

            builder.Conventions.SetPropertyMetadata(
                attribute: _ => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );
            builder.Conventions.AddPropertyMetadataConfiguration<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );

            builder.Conventions.SetMethodMetadata(
                attribute: _ => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );
            builder.Conventions.AddMethodMetadataConfiguration<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );

            builder.Conventions.SetParameterMetadata(
                attribute: _ => new CustomAttribute(),
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );
            builder.Conventions.AddParameterMetadataConfiguration<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );

            builder.Conventions.AddTypeComponent(
                component: (_, cc) => ReportPage("test-page", PageTitle("Test Page")),
                whenType: c => c.Type.Is<TestPage>(),
                whenComponent: cc => cc.Path.EndsWith(nameof(Page))
            );
            builder.Conventions.AddTypeComponentConfiguration<ReportPageC>(
                component: (reportPage, c, cc) => reportPage.Schema.Tabs.AddRange(
                    c.Type.GetSchemas<ReportPageC.Tab>(cc.Drill(nameof(ReportPageC.Tabs)))
                ),
                whenType: c => c.Type.Is<TestPage>()
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => ReportPageTab("default"),
                whenType: c => c.Type.Is<TestPage>(),
                whenComponent: cc => cc.Path.EndsWith(nameof(ReportPageC.Tabs))
            );
            builder.Conventions.AddTypeSchemaConfiguration<ReportPageC.Tab>(
                schema: (tab, c, cc) => tab.Contents.Add(
                    c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GetSchema<ReportPageC.Tab.Content>(cc.Drill(tab.Id, nameof(ReportPageC.Tab.Contents), 0))
                        ?? throw new($"{nameof(TestPage.GetData)} is expected to have a report page content")
                ),
                whenType: c => c.Type.Is<TestPage>()
            );

            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => ReportPageTabContent(component: c.Method.GetRequiredComponent(cc.Drill(nameof(ReportPageC.Tab.Content.Component))), c.Method.Name.Kebaberize()),
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                whenComponent: c => c.Path.EndsWith(nameof(ReportPageC.Tab.Contents), 0)
            );
            builder.Conventions.AddMethodSchemaConfiguration<ReportPageC.Tab.Content>(
                schema: tabContent => tabContent.Narrow = true,
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodString(c.Method, cc),
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                whenComponent: cc => cc.Path.EndsWith(nameof(ReportPageC.Tab.Content.Component))
            );
            builder.Conventions.AddMethodComponentConfiguration<Ui.Component.String>(
                component: (@string) => @string.Schema.MaxLength = 20,
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, SampleExceptionHandler>();
            services.AddHostedService<SeedDataTrigger>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("samples", new() { Title = "Samples", Version = "v1" });
            swaggerGenOptions.SwaggerDoc("external", new() { Title = "External", Version = "v1" });

            swaggerGenOptions.DocInclusionPredicate((documentName, api) =>
                documentName == "samples" ||
                documentName == "external" && api.GroupName == "ExternalSamples"
            );

            swaggerGenOptions.AddSecurityDefinition("Custom",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Custom-API-Key",
                    Description = "Demonstration of additional security definitions",
                },
                documentName: "external"
            );
            swaggerGenOptions.AddSecurityDefinition("Custom.Secret",
                new()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Custom-API-Secret",
                    Description = "Demonstration of adding two requirements",
                },
                documentName: "external"
            );

            swaggerGenOptions.AddSecurityRequirementToOperationsThatUse<AuthorizeAttribute>(["Custom", "Custom.Secret"], documentName: "external");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>(new() { Name = "X-Security-2", In = ParameterLocation.Header }, documentName: "external");
            swaggerGenOptions.AddParameterToOperationsThatUse<AuthorizeAttribute>(new() { Name = "X-Security-1", In = ParameterLocation.Header }, position: 0, documentName: "external");
        });

        configurator.ConfigureSwaggerUIOptions(swaggerUIOptions =>
        {
            swaggerUIOptions.SwaggerEndpoint($"samples/swagger.json", "Samples");
            swaggerUIOptions.SwaggerEndpoint($"external/swagger.json", "External");
        });
    }
}