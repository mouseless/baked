using Baked.Theme;

namespace Baked.Test.Theme.Custom;

public static class Components
{
    public static ComponentDescriptorAttribute<Container> Container(
        Action<Container>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptorAttribute<Expected> Expected(string testId,
        Action<Expected>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptorAttribute<Input> Input(string testId,
        Action<Input>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptorAttribute<LoginPage> LoginPage(string path,
        Action<LoginPage>? options = default
    ) => new(options.Apply(new(path)));

    public static ComponentDescriptorAttribute<RoutedPage> RoutedPage(string path,
        Action<RoutedPage>? options = default
    ) => new(options.Apply(new(path)));
}