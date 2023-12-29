using DomainModelOverReflection.Domain.Business;

namespace DomainModelOverReflection.Test.Business;

public class TestEntities
{
    readonly IQueryContext<TestEntity> _context;

    public TestEntities(IQueryContext<TestEntity> context) => _context = context;

    public List<TestEntity> All() => _context.All();
}
