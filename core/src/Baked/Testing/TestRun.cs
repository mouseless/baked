using Baked.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Testing;

public class TestRun(TestConfiguration _configuration)
    : ITestRun, IServiceProviderAccessor
{
    IServiceScope? _currentScope;

    public IServiceProvider? GetServiceProvider() =>
        _currentScope?.ServiceProvider;

    public void SetUp(Spec spec)
    {
        _currentScope = spec.GiveMe.TheServiceProvider().CreateScope();

        foreach (var setUp in _configuration.SetUps)
        {
            setUp(spec);
        }
    }

    public void TearDown(Spec spec)
    {
        foreach (var tearDown in _configuration.TearDowns)
        {
            tearDown(spec);
        }

        _currentScope?.Dispose();
        _currentScope = null;
    }
}