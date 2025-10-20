using System.Diagnostics.CodeAnalysis;

namespace Baked.Ui;

[return: NotNullIfNotNull(nameof(key))]
public delegate string? NewLocaleKey(string? key);