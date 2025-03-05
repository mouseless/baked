namespace Baked.Ui;

public static class Datas
{
    public static ComputedData Computed(string composable) =>
        new(composable);

    public static InlineData Inline(object value) =>
        new(value);

    public static RemoteData Remote(string path) =>
        new(path);
}