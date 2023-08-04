using Moq;

namespace Do.Testing;

public class DefaultMockFactory : IMockFactory
{
    public virtual object Create(IServiceProvider serviceProvider, MockDescriptor mockDescriptor)
    {
        var mockType = typeof(Mock<>).MakeGenericType(mockDescriptor.Type);
        var mockInstance = (Mock)Activator.CreateInstance(mockType)!;

        mockDescriptor.Setup?.Invoke(mockInstance);

        return mockInstance.Object;
    }
}
