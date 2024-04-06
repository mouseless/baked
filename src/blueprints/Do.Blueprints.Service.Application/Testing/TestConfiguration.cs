namespace Do.Testing;

public record TestConfiguration()
{
    public IMockCollection Mocks { get; } = new MockCollection();
    public IMockFactory MockFactory { get; set; } = new DefaultMockFactory();
}