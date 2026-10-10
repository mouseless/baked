using Baked.Buildtime;
using Baked.Domain.Model;

namespace Baked.CodingStyle.SuffixBasedClient;

public class ClientTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings = [];

    readonly IEnumerable<TypeModel> _clients;

    public ClientTemplate(DomainModel domain)
    {
        _clients = domain.Types.Having<ClientAttribute>();

        AddReferences(_clients);
    }

    protected override IEnumerable<string> Render() =>
        [ClientTypes()];

    string ClientTypes() => $$"""
        namespace SuffixBasedClientCodingStyleFeature;

        public class Clients : List<Type>
        {
            public Clients()
            {
                AddRange(
                [
        {{ForEach(_clients, client => $$"""
                    typeof({{client.CSharpFriendlyFullName}})
        """, separator: $",{Environment.NewLine}", indentation: 1)}}
                ]);
            }
        }
    """;
}