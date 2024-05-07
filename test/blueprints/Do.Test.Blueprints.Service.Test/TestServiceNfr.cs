using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using Do.Business;
using Do.Database;
using Do.Test.CodingStyle.EntitySubclassViaComposition;
using Do.Test.Orm;
using Do.Testing;

namespace Do.Test;

public abstract class TestServiceNfr : ServiceNfr<TestServiceNfr>, IEntryPoint
{
    public static void Main(string[] args) => Init(args);

    protected override IEnumerable<string> EntityNamesToClearOnTearDown =>
        [nameof(Entity), nameof(Parent), nameof(TypedEntity)];

    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
       [
           c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Jane", claims: ["User"]);
                tokens.Add("John", claims: ["User", "Admin"]);
            })
       ];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(claims: ["User", "Admin"]);
    protected override Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business =>
        c => c.DomainAssemblies([typeof(Entity).Assembly]);
    protected override Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database =>
        c => c.Sqlite(fileName: $"{nameof(TestServiceNfr)}.db");
    protected override Action<ApplicationDescriptor>? Configure =>
        app => app.Features.AddConfigurationOverrider();

    [TearDown]
    public override void TearDown()
    {
        Client.DefaultRequestHeaders.Clear();
    }
}