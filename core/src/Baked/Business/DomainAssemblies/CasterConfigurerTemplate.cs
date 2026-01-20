using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Business.DomainAssemblies;

public class CasterConfigurerTemplate(DomainModel _domain) : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.Business.DomainAssemblies",
            "Baked.Runtime",
            "Microsoft.Extensions.DependencyInjection"
        ];

    protected override IEnumerable<string> Render() =>
        [Caster()];

    string Caster() => $$"""
    namespace DomainAssembliesFeature;

    public class CasterConfigurer: ICasterConfigurer
    {
        public void Configure()
        {
            {{ForEach(_domain.Types.Having<CasterAttribute>(), type =>
                ForEach(type.GetInheritance().Interfaces.Where(i => i.Model.IsGenericType && !i.Model.IsGenericTypeDefinition && i.Model.IsAssignableTo(typeof(ICasts<,>))), @interface =>
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