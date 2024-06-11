using System.Reflection;

namespace Baked.Domain;

public interface IDomainTypeCollection : IList<Type>
{
    void AddFromAssembly(Assembly assembly, Func<Type, bool> except);
}