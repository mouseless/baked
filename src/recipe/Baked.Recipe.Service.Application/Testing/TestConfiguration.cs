namespace Baked.Testing;

public record TestConfiguration()
{
    public IMockCollection Mocks { get; } = new MockCollection();
    public IMockFactory MockFactory { get; set; } = new DefaultMockFactory();
    public List<Action<Spec>> SetUps { get; } = [];
    public List<Action<Spec>> TearDowns { get; } = [];
}