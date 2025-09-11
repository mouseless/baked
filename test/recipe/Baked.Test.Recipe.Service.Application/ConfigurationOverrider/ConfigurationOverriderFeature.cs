using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.RestApi.Model;
using Baked.Test.Authentication;
using Baked.Test.Business;
using Baked.Test.ExceptionHandling;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using static Baked.Theme.Admin.Components;
using static Baked.Theme.Admin.DomainComponents;
using static Baked.Test.Theme.Custom.DomainDatas;

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
            builder.Conventions.AddTypeConvention<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) => c.Type.Is<Class>()
            );

            builder.Conventions.SetPropertyMetadata(
                attribute: _ => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );
            builder.Conventions.AddPropertyConvention<CustomAttribute>(
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
            builder.Conventions.AddMethodConvention<CustomAttribute>(
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
            builder.Conventions.AddParameterConvention<CustomAttribute>(
                apply: attr => attr.Value = "FROM CONVENTION",
                when: (_, c) =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );

            builder.Conventions.AddTypeComponent(
                component: (_, cc) => ReportPage("test-page", PageTitle("Test Page")),
                whenType: c => c.Type.Is<TestPage>(),
                whenComponent: cc => cc.Path == "/page"
            );
            builder.Conventions.AddTypeComponentConvention<ReportPage>(
                component: (reportPage, c, cc) => reportPage.Schema.Tabs.AddRange(
                    c.Type.GetSchemas<ReportPage.Tab>(cc.CreateComponentContext("/tabs"))
                ),
                whenType: c => c.Type.Is<TestPage>()
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => ReportPageTab("default"),
                whenType: c => c.Type.Is<TestPage>(),
                whenComponent: cc => cc.Path.EndsWith("/tabs")
            );
            builder.Conventions.AddTypeSchemaConvention<ReportPage.Tab>(
                schema: (tab, c, cc) => tab.Contents.Add(
                    c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GetSchema<ReportPage.Tab.Content>(cc.CreateComponentContext($"/{tab.Id}/contents/0"))
                        ?? throw new($"{nameof(TestPage.GetData)} is expected to have a report page content")
                ),
                whenType: c => c.Type.Is<TestPage>()
            );

            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => ReportPageTabContent(component: c.Method.GetComponent(cc.CreateComponentContext($"/component"))),
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                whenComponent: c => c.Path.EndsWith("/contents/0")
            );
            builder.Conventions.AddMethodSchemaConvention<ReportPage.Tab.Content>(
                schema: tabContent => tabContent.Narrow = true,
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => String(data: ActionRemote(c.Method)),
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                whenComponent: cc => cc.Path.EndsWith("/component")
            );
            builder.Conventions.AddMethodComponentConvention<Baked.Theme.Admin.String>(
                component: (@string) => @string.Schema.MaxLength = 20,
                whenMethod: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );

            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "Single Value", options: rpt => rpt.Icon = Icon("pi-box")),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("/tabs")
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => TypeReportPageTab(c.Type, cc, "Data Table", options: rpt => rpt.Icon = Icon("pi-table")),
                whenType: c => c.Type.Is<Report>(),
                whenComponent: cc => cc.Path.EndsWith("/tabs")
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