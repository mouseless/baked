namespace Do.HttpServer;

public interface IMiddlewareCollection : ICollection<MiddlewareDescriptor>, IEnumerable<MiddlewareDescriptor>, IEnumerable, IList<MiddlewareDescriptor> { }
