using Microsoft.Extensions.FileProviders;

namespace Baked.Runtime;

public record FileProviderDescriptor(object? Key, IFileProvider Provider);