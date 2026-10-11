using Baked.Buildtime;
using Baked.Business;
using Baked.Domain.Model;

namespace Baked.CodingStyle.PrimitiveViaParsable;

public class PrimitiveTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings = [];

    readonly IEnumerable<TypeModel> _valueTypes;

    public PrimitiveTemplate(DomainModel domain)
    {
        _valueTypes = domain.Types.Having<Primitive>();

        AddReferences(_valueTypes);
    }

    protected override IEnumerable<string> Render() =>
        [Primitives()];

    string Primitives() => $$"""
        namespace PrimitiveViaParsableCodingStyleFeature;

        public class Primitives : List<Type>
        {
            public Primitives()
            {
                AddRange(
                [
        {{ForEach(_valueTypes, valueType => $$"""
                    typeof({{valueType.CSharpFriendlyFullName}})
        """, separator: $",{Environment.NewLine}", indentation: 1)}}
                ]);
            }
        }
    """;
}