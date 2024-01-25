using Do.Architecture;
using Do.Business;
using Do.Database;
using Do.Test.RestApi.Analyzer;
using Do.Testing;

namespace Do.Test;

public abstract class TestServiceNfr : ServiceNfr<TestServiceNfr>, IEntryPoint
{
    public static void Main(string[] args) => Init(args);

    protected override Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business =>
        c => c.Default(assemblies: [typeof(Entity).Assembly], controllerAssembly: typeof(ParentsController).Assembly);
    protected override Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database =>
        c => c.InMemory();
}
