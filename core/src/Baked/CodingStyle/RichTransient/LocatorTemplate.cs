using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.RichTransient;

public class LocatorTemplate(TypeModel typeModel, bool isAsync)
    : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
        ];

    protected override IEnumerable<string> Render() =>
        [Locator()];

    string Locator() => $$"""
    namespace RichTransient;

    public class {{typeModel.Name}}Locator({{Factory}})
        : {{LocatorTypeName}}
    {
        public {{ReturnType}} Locate(Id id, bool _) =>
            _new{{typeModel.Name}}().With(id);

        public IEnumerable<{{ReturnType}}> LocateMany(IEnumerable<Id> ids) =>
            ids.Select(id => _new{{typeModel.Name}}().With(id));
    }
    """;

    string ReturnType => isAsync ? $$"""Task<{{typeModel.CSharpFriendlyFullName}}>""" : $$"""{{typeModel.CSharpFriendlyFullName}}""";
    string Factory => $$"""Func<{{typeModel.CSharpFriendlyFullName}}> _new{{typeModel.Name}}""";
    public string LocatorTypeName => $$"""ILocator<{{ReturnType}}>""";
    public string ImplementatonTypeName => $$"""RichTransient.{{typeModel.Name}}Locator""";
}