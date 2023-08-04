using Do.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Do.MockOverrider.FirstInterface;

public class MockOverriderMockFactory : DefaultMockFactory
{
    public override object Create(IServiceProvider serviceProvider, MockDescriptor mockRegistration)
    {
        var overrider = serviceProvider.GetRequiredService<MockOverrider>();

        return overrider.Get(mockRegistration.Type) ?? base.Create(serviceProvider, mockRegistration);
    }
}
