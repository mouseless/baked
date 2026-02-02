using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LocatorTemplate(TypeModel _entityExtension)
    : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "Baked.Orm"
        ];

    protected override IEnumerable<string> Render() =>
        [Locator()];

    string Locator() => $$"""
    namespace EntityExtensionViaComposition;

    public class {{_entityExtension.Name}}Locator(ILocator<{{EntityName}}> _entityLocator)
        : ILocator<{{_entityExtension.CSharpFriendlyFullName}}>
    {
        public {{_entityExtension.CSharpFriendlyFullName}} Locate(Baked.Business.Id id, bool throwNotFound) =>
            ({{_entityExtension.CSharpFriendlyFullName}})_entityLocator.Locate(id, throwNotFound: throwNotFound);

        public IEnumerable<{{_entityExtension.CSharpFriendlyFullName}}> LocateMany(IEnumerable<Baked.Business.Id> ids) =>
            _entityLocator.LocateMany(ids).Select(e => ({{_entityExtension.CSharpFriendlyFullName}})e);
    }
    """;

    string EntityName => _entityExtension.GetMetadata().Get<EntityExtensionAttribute>().EntityType.GetCSharpFriendlyFullName();
    public string LocatorTypeName => $$"""ILocator<{{_entityExtension.CSharpFriendlyFullName}}>""";
    public string ImplementatonTypeName => $$"""EntityExtensionViaComposition.{{_entityExtension.Name}}Locator""";
}