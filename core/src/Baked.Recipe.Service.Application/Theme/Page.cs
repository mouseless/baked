namespace Baked.Theme;

public class Page
{
    public class Generator;
    public class Describer;

    public PageBuilder Generated(Func<Generator, PageBuilder> generate) =>
        generate(new());

    public PageBuilder Described(Func<Describer, PageBuilder> describe) =>
        describe(new());

    public PageBuilder? Implemented() =>
        null;
}