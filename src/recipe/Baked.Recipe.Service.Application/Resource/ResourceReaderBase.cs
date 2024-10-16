﻿using Microsoft.Extensions.FileProviders;
using System.Text;

namespace Baked.Resource;

public abstract class ResourceReaderBase(IEnumerable<IFileProvider> providers)
    : CompositeFileProvider(providers)
{
    public string? ReadAsString(string subPath)
    {
        var fileInfo = GetFileInfo(subPath);
        if (!fileInfo.Exists) { return null; }

        using var stream = GetFileInfo(subPath).CreateReadStream();
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }

    public async Task<string?> ReadAsStringAsync(string subPath)
    {
        var fileInfo = GetFileInfo(subPath);
        if (!fileInfo.Exists) { return null; }

        using var stream = fileInfo.CreateReadStream();
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return await streamReader.ReadToEndAsync();
    }
}