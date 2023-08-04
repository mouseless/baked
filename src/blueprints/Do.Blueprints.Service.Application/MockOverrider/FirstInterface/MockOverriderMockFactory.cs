using Do.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Do.MockOverrider.FirstInterface;

public class MockOverriderMockFactory : DefaultMockFactory
{
    public override object Create(IServiceProvider serviceProvider, MockDescriptor mockDescriptor)
    {
        var overrider = serviceProvider.GetRequiredService<MockOverrider>();

        return overrider.Get(mockDescriptor.Type) ?? base.Create(serviceProvider, mockDescriptor);
    }
}
