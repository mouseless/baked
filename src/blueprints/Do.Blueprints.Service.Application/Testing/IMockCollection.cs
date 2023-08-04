namespace Do.Testing;

public interface IMockCollection : ICollection<MockDescriptor>, IEnumerable<MockDescriptor>, IEnumerable, IList<MockDescriptor> { }
