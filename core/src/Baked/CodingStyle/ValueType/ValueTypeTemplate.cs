using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings = [];

    readonly IEnumerable<TypeModel> _valueTypes;

    public ValueTypeTemplate(DomainModel domain)
    {
        _valueTypes = domain.Types.Having<ValueTypeAttribute>();

        AddReferences(_valueTypes);
    }

    protected override IEnumerable<string> Render() =>
        [ValueTypes()];

    string ValueTypes() => $$"""
        namespace ValueTypeCodingStyleFeature;

        public class ValueTypes : List<Type>
        {
            public ValueTypes()
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