using System.Reflection;

namespace Do.Domain;

public interface IDomainTypeCollection : IList<Type>
{
    void AddFromAssembly(Assembly assembly, Func<Type, bool> except);
}
