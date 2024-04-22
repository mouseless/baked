using Do.Architecture;
using Do.Authentication;
using Do.Authentication.FixedToken;
using Do.Authorization;
using Do.Business;
using Do.Database;
using Do.Test.Orm;
using Do.Testing;

namespace Do.Test;

public abstract class TestServiceNfr : ServiceNfr<TestServiceNfr>, IEntryPoint
{
    public static void Main(string[] args) => Init(args);

    protected override IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications =>
       [c => c.FixedToken(_optionsBuilder: options => { options.IdentityOptions.Add("Admin", [new TokenClaimProvider()]); })];
    protected override Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization =>
        c => c.ClaimBased(policies:
            new()
            {
                 { "AdminOnly", policy => policy.RequireClaim("Token") },
                 { "ManagerOnly", policy => policy.RequireClaim("Manager") }
            }
        );
    protected override Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business =>
        c => c.DomainAssemblies([typeof(Entity).Assembly]);
    protected override Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database =>
        c => c.Sqlite(fileName: $"{nameof(TestServiceNfr)}.db");
    protected override Action<ApplicationDescriptor>? Configure =>
        app => app.Features.AddConfigurationOverrider();
}