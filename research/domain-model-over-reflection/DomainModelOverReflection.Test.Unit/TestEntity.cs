using DomainModelOverReflection.Domain.Business;

namespace DomainModelOverReflection.Test.Business;

public class TestEntity
{
    readonly IEntityContext<TestEntity> _context = default!;

    protected TestEntity() { }

    public TestEntity(IEntityContext<TestEntity> context) => _context = context;

    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = default!;
    public virtual DateTime CreatedAt { get; set; }

    protected internal virtual TestEntity With() => _context.Insert(this);

    public virtual void Method(string text) { }
    protected internal virtual void InternalMethod() { }
    protected virtual void ProtectedMethod() { }
}
