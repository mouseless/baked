using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.MockOverrider.FirstInterface;

public class MockOverriderMockFactory : DefaultMockFactory
{
    public override object Create(IServiceProvider serviceProvider, MockDescriptor mockDescriptor)
    {
        var overrider = serviceProvider.GetRequiredService<MockOverrider>();

        var result = overrider.Get(mockDescriptor.Type) ?? base.Create(serviceProvider, mockDescriptor);

        if (mockDescriptor.Singleton)
        {
            overrider.ResetEventually(mockDescriptor, result);
        }

        return result;
    }
}