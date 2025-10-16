using Baked.Ui;

namespace Baked.Test.Ui;

public record RoutedPage(string Path)
    : PageSchemaBase(Path);