using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.RichTransient;

public class LocatorTemplate(TypeModel typeModel, bool isAsync) : CodeTemplateBase
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

    public class {{Implementaton}}({{Factory}}) : {{ILocator}}
    {
        public IEnumerable<{{ReturnType}}> Multiple(IEnumerable<Id> ids)
        {
            return ids.Select(id => _new{{typeModel.Name}}().With(id));
        }

        public {{ReturnType}} Single(Id id, bool _)
        {
            return _new{{typeModel.Name}}().With(id);
        }
    }
    """;

    string ReturnType => isAsync ? $$"""Task<{{typeModel.CSharpFriendlyFullName}}>""" : $$"""{{typeModel.CSharpFriendlyFullName}}""";
    string Factory => $$"""Func<{{typeModel.CSharpFriendlyFullName}}> _new{{typeModel.Name}}""";
    public string ILocator => $$"""ILocator<{{ReturnType}}>""";
    public string Implementaton => $$"""{{typeModel.Name}}Locator""";
}