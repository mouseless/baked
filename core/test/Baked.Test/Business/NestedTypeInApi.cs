namespace Baked.Test.Business;

public class NestedTypeInApi
{
    public record Input(string Text);
    public record Result(string Text);

    public Result Execute(Input input) =>
        new(input.Text);
}
