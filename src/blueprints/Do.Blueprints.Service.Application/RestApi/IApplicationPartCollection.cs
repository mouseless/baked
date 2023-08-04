namespace Do.RestApi;

public interface IApplicationPartCollection : ICollection<ApplicationPartDescriptor>, IEnumerable<ApplicationPartDescriptor>, IEnumerable, IList<ApplicationPartDescriptor> { }
