using Baked.Architecture;
using Baked.Binding;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Lifetime;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.CommandViaMethodName;

public class CommandViaMethodNameCodingStyleFeature(IEnumerable<string> _methodNames)
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
                        !members.Has<Transient>() ||
                        members.Has<Transient>() && !members.Has<Locatable>()
                    ) &&
                    TryGetSinglePotentialAction(members, c, out var action) &&
                    _methodNames.Contains(action.Name),
                apply: (c, set) =>
                {
                    set(c.Type, new Command());

                    var members = c.Type.GetMembers();
                    foreach (var method in members.Methods)
                    {
                        if (!_methodNames.Contains(method.Name)) { continue; }

                        set(method, new CommandMethod());
                    }
                },
                order: Order.At.Infra + 40
            );
            conventions.RemoveTypeAttribute<ApiController>(
                when: c =>
                    c.Type.Has<Command>() &&
                    c.Type.Has<Transient>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Any(m =>
                        m.Has<Initializer>() &&
                        m.DefaultOverload.DeclaringType == c.Type &&
                        m.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                        !m.DefaultOverload.AllParametersAreApiInput()
                    ),
                order: Order.At.Infra + 40
            );

            conventions.Add(new IncludeClassDocsForActionNamesConvention(
                _whenContext: c => c.Method.Has<CommandMethod>()
            ), order: Order.At.Infra - 10);

            conventions.Add(new UseClassNameInsteadOfActionNamesConvention(
                _whenContext: c => c.Method.Has<CommandMethod>()
            ), order: Order.At.Infra - 10);

            conventions.Add(new RemoveFromRouteConvention(
                _parts: _methodNames,
                _whenContext: c => c.Method.Has<CommandMethod>()
            ), order: Order.At.Infra);

            conventions.Add(new RemoveFromRouteConvention(
                _parts: ["Sync", "Create"],
                _whenContext: c => c.Method.Has<CommandMethod>()
            ), order: Order.At.Infra);

            conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention(
                _whenContext: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<Command>()
            ), order: Order.At.Infra);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Sync"),
                _whenContext: c => c.Method.Has<CommandMethod>(),
                _method: HttpMethod.Put
            ), order: Order.At.Infra - 10);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Create"),
                _whenContext: c => c.Method.Has<CommandMethod>(),
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

    static bool IsPotentialAction(MethodModel m, TypeModelMetadataContext c) =>
        !m.Has<Initializer>() &&
        m.DefaultOverload.DeclaringType == c.Type &&
        m.DefaultOverload.IsPublicInstanceWithNoSpecialName;

    bool TryGetSinglePotentialAction(
        TypeModelMembers members,
        TypeModelMetadataContext c,
        [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out MethodModel? action
    )
    {
        action = null;
        var found = false;

        foreach (var m in members.Methods)
        {
            if (!IsPotentialAction(m, c)) { continue; }
            if (found)
            {
                action = null;

                return false;
            }

            action = m;
            found = true;
        }

        return found;
    }
}