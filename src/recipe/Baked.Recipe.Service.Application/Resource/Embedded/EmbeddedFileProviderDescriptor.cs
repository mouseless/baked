using System.Reflection;

namespace Baked.Resource.Embedded;

public record EmbeddedFileProviderDescriptor(Assembly Assembly, string? BaseNamespace);