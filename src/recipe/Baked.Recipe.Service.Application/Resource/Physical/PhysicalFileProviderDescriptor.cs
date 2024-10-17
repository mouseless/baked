using Baked.Runtime;
using Microsoft.Extensions.FileProviders;

namespace Baked.Resource.Physical;

public record class PhysicalFileProviderDescriptor(object Key, string? Root) :
    FileProviderDescriptor(Key, new PhysicalFileProvider(Root ?? string.Empty));