using System.Reflection;

namespace Do.Domain;

public interface IAssemblyCollection : ICollection<Assembly>, IEnumerable<Assembly>, IEnumerable, IList<Assembly> { }
