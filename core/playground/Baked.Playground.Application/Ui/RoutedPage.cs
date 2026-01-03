using Baked.Ui;

namespace Baked.Playground.Ui;

public record RoutedPage(string Path)
    : PageSchemaBase(Path);