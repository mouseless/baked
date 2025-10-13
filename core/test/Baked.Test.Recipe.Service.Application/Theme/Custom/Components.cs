using Baked.Ui;

namespace Baked.Test.Theme.Custom;

public static class Components
{
    public static ComponentDescriptor<Container> Container(
        Action<Container>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptor<Expected> Expected(string testId,
        Action<Expected>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptor<Input> Input(string testId,
        Action<Input>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptor<LoginPage> LoginPage(string path,
        Action<LoginPage>? options = default
    ) => new(options.Apply(new(path)));

    public static ComponentDescriptor<RoutedPage> RoutedPage(string path,
        Action<RoutedPage>? options = default
    ) => new(options.Apply(new(path)));
}