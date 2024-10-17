using Microsoft.Extensions.FileProviders;

namespace Baked.Runtime;

public class FileProviderCollection : List<IFileProvider>, IFileProviderCollection;

public interface IFileProviderCollection : IList<IFileProvider>;