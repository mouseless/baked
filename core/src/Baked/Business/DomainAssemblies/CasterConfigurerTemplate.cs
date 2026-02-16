using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Business.DomainAssemblies;

public class CasterConfigurerTemplate : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.Business.DomainAssemblies",
            "Baked.Runtime",
            "Microsoft.Extensions.DependencyInjection"
        ];

    readonly DomainModel _domain;
    readonly IEnumerable<TypeModelInheritance> _types;

    public CasterConfigurerTemplate(DomainModel domain)
    {
        _domain = domain;
        _types = domain.Types
            .Having<CasterAttribute>()
            .Where(t => t.HasInheritance())
            .Select(t => t.GetInheritance());

        AddReferences(_types);
    }

    protected override IEnumerable<string> Render() =>
        [Caster()];

    string Caster() => $$"""
    namespace DomainAssembliesFeature;

    public class CasterConfigurer: ICasterConfigurer
    {
        public void Configure()
        {
            {{ForEach(_types, type =>
                ForEach(type.Interfaces.Where(i => i.Model.IsGenericType && !i.Model.IsGenericTypeDefinition && i.Model.IsAssignableTo(typeof(ICasts<,>))), @interface =>
                {
                    var result = string.Empty;
                    type.Apply(t => @interface.Apply(i =>
                    {
                        result = $$"""
                            Caster.Add(
                                typeof({{_domain.Types[i.GenericTypeArguments[0]].CSharpFriendlyFullName}}),
                                typeof({{_domain.Types[i.GenericTypeArguments[1]].CSharpFriendlyFullName}}),
                                sp => sp.UsingCurrentScope().GetRequiredService<{{type.CSharpFriendlyFullName}}>()
                            );
                        """;
                    }));

                    return result;
                }
                )
            )}}
        }
    }
    """;
}