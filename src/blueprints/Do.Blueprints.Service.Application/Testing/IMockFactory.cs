namespace Do.Testing;

public interface IMockFactory
{
    object Create(IServiceProvider serviceProvider, MockDescriptor mockDescriptor);
}