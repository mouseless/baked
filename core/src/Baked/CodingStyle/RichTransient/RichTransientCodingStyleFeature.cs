using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Baked.Runtime;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Has<TransientAttribute>() &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.IsPublic &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.All(p =>
                            p.Name == "id" &&
                            (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                        )
                    ),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());
                    set(c.Type, new LocatableAttribute());
                },
                order: 10
            );
            builder.Conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Has<TransientAttribute>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.IsPublic) &&
                    c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublic &&
                    c.Method.DefaultOverload.Parameters.Count == 1 &&
                    c.Method.DefaultOverload.Parameters.All(p =>
                        p.Name == "id" && (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                    ),
                attribute: c => new ActionModelAttribute(),
                order: 20
            );

            builder.Conventions.Add(new RichTransientUnderPluralGroupConvention());
            builder.Conventions.Add(new LocateUsingInitializerConvention(), order: 10);
            builder.Conventions.Add(new LocateUsingLocatorConvention(), order: 10);
            builder.Conventions.Add(new RichTransientInitializerIsGetResourceConvention(), order: 10);
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(RichTransientCodingStyleFeature),
                    assembly =>
                    {
                        List<(string, string)> locators = [];
                        foreach (var item in domain.Types.Having<TransientAttribute>())
                        {
                            if (item.GetMembers().Methods.Any(m =>
                                    m.Has<InitializerAttribute>() &&
                                    m.DefaultOverload.IsPublic &&
                                    m.DefaultOverload.Parameters.Count == 1 &&
                                    m.DefaultOverload.Parameters.All(p => p.Name == "id" && (p.ParameterType.IsValueType || p.ParameterType.Is<string>()))
                                ) &&
                                item.GetMetadata().TryGet<LocatableAttribute>(out var locatable)
                            )
                            {
                                var codeTemplate = new LocatorTemplate(item, locatable.IsAsync);
                                assembly.AddCodes(codeTemplate);
                                item.Apply(t => assembly.AddReferenceFrom(t));
                                locators.Add((codeTemplate.ILocator, codeTemplate.Implementaton));
                            }
                        }

                        assembly.AddCodes(new LocatorAdderTemplate(locators));
                        assembly.AddReferenceFrom<RichTransientCodingStyleFeature>();
                    },
                    usings: [.. LocatorTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                var locatorAdderType = context.Assemblies[nameof(RichTransientCodingStyleFeature)].GetExportedTypes().First(t => t.IsAssignableTo(typeof(IServiceAdder)));
                if (locatorAdderType is not null)
                {
                    var locatorAdder = (IServiceAdder?)Activator.CreateInstance(locatorAdderType) ?? throw new($"Cannot create instance of {locatorAdderType}");

                    locatorAdder.AddServices(services);
                }
            });
        });
    }
}