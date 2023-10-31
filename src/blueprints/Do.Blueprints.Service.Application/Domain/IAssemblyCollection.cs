namespace Do.Domain;

public interface IAssemblyCollection : ICollection<AssemblyDescriptor>, IEnumerable<AssemblyDescriptor>, IEnumerable, IList<AssemblyDescriptor> { }
