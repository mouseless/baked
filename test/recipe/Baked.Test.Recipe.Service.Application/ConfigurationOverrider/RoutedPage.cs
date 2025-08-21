using Baked.Ui;

namespace Baked.Test.ConfigurationOverrider;

public record RoutedPage(string Path)
    : PageSchemaBase(Path);