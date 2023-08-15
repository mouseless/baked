using Moq;

namespace Do.Testing;

public class DefaultMockFactory : IMockFactory
{
    public virtual object Create(IServiceProvider serviceProvider, MockDescriptor mockDescriptor)
    {
        var mockType = typeof(Mock<>).MakeGenericType(mockDescriptor.Type);
        var mockInstance = (Mock?)Activator.CreateInstance(mockType)
            ?? throw new ArgumentException($"Activator could not create instance of '{mockType}'", nameof(mockDescriptor));

        mockDescriptor.Setup?.Invoke(mockInstance);

        return mockInstance.Object;
    }
}
