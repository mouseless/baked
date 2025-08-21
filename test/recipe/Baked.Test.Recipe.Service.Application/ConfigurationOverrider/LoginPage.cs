using Baked.Ui;

namespace Baked.Test.ConfigurationOverrider;

public record LoginPage(string Path)
    : PageSchemaBase(Path);