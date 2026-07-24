using Baked.Architecture;
using Baked.Binding;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.CommandPattern;

public class CommandPatternCodingStyleFeature(IEnumerable<string> _methodNames)
    : IFeature<CodingStyleConfigurator>
{
    readonly HashSet<string> _methodNames = [.. _methodNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    (
                        !members.Has<TransientAttribute>() ||
                        members.Has<TransientAttribute>() && !members.Has<LocatableAttribute>()
                    ) &&
                    members.Methods.Count(m => IsPotentialAction(m, c)) == 1 &&
                    _methodNames.Contains(members.Methods.Single(m => IsPotentialAction(m, c)).Name),
                apply: (c, set) =>
                {
                    set(c.Type, new CommandAttribute());

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods)
                    {
                        if (!_methodNames.Contains(method.Name)) { continue; }

                        set(method, new CommandMethodAttribute());
                    }
                },
                order: Order.At.Infra + 40
            );
            conventions.RemoveTypeAttribute<ControllerModelAttribute>(
                when: c =>
                    c.Type.Has<CommandAttribute>() &&
                    c.Type.Has<TransientAttribute>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.DeclaringType == c.Type &&
                        m.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                        !m.DefaultOverload.AllParametersAreApiInput()
                    ),
                order: Order.At.Infra + 40
            );

            conventions.Add(new IncludeClassDocsForActionNamesConvention(
                _whenContext: c => c.Method.Has<CommandMethodAttribute>()
            ), order: Order.At.Infra - 10);

            conventions.Add(new UseClassNameInsteadOfActionNamesConvention(
                _whenContext: c => c.Method.Has<CommandMethodAttribute>()
            ), order: Order.At.Infra - 10);

            conventions.Add(new RemoveFromRouteConvention(
                _parts: _methodNames,
                _whenContext: c => c.Method.Has<CommandMethodAttribute>()
            ), order: Order.At.Infra);

            conventions.Add(new RemoveFromRouteConvention(
                _parts: ["Sync", "Create"],
                _whenContext: c => c.Method.Has<CommandMethodAttribute>()
            ), order: Order.At.Infra);

            conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention(
                _whenContext: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<CommandAttribute>()
            ), order: Order.At.Infra);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Sync"),
                _whenContext: c => c.Method.Has<CommandMethodAttribute>(),
                _method: HttpMethod.Put
            ), order: Order.At.Infra - 10);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Create"),
                _whenContext: c => c.Method.Has<CommandMethodAttribute>(),
                _method: HttpMethod.Patch
            ), order: Order.At.Infra - 10);
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesFromClassOperationFilter>(_methodNames, examples);
            });
        });
    }

    bool IsPotentialAction(MethodModel m, TypeModelMetadataContext c) =>
        !m.Has<InitializerAttribute>() &&
        m.DefaultOverload.DeclaringType == c.Type &&
        m.DefaultOverload.IsPublicInstanceWithNoSpecialName;
}