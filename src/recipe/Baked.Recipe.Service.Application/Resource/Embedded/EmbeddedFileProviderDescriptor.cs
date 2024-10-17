using Baked.Runtime;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Resource.Embedded;

public record EmbeddedFileProviderDescriptor(object Key, Assembly Assembly, string? BaseNamespace) :
    FileProviderDescriptor(Key, new EmbeddedFileProvider(Assembly, BaseNamespace));