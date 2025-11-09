using Baked.Ui;

namespace Baked.Test.Ui;

public static class Components
{
    public static ComponentDescriptor<Container> Container(
        Action<Container>? options = default
    ) => new(options.Apply(new()));

    public static ComponentDescriptor<ContainerPage> ContainerPage(string path,
        Action<ContainerPage>? options = default
    ) => new(options.Apply(new(path)));

    public static ComponentDescriptor<Expected> Expected(string testId,
        Action<Expected>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptor<InputText> InputText(string testId,
        Action<InputText>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptor<InputNumber> InputNumber(string testId,
        Action<InputNumber>? options = default
    ) => new(options.Apply(new(testId)));

    public static ComponentDescriptor<LoginPage> LoginPage(string path,
        Action<LoginPage>? options = default
    ) => new(options.Apply(new(path)));

    public static ComponentDescriptor<RoutedPage> RoutedPage(string path,
        Action<RoutedPage>? options = default
    ) => new(options.Apply(new(path)));

    // TODO - review this in form components
    public static ComponentDescriptor<VibeForm> VibeForm(
        Action<VibeForm>? options = default
    ) => new(options.Apply(new()));
}