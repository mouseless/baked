using Do.Architecture;
using Do.Testing;

namespace Do.Test;

public static class ApplicationContextExtensions
{
    public static ApplicationContext AnApplicationContext(this Stubber _) => new();
    public static ApplicationContext AnApplicationContext<T>(this Stubber giveMe, T content) where T : notnull
    {
        var result = giveMe.AnApplicationContext();

        result.Add(content);

        return result;
    }

    public static ApplicationContext AnApplicationContext<T1, T2>(this Stubber giveMe, T1 content1, T2 content2)
        where T1 : notnull
        where T2 : notnull
    {
        var result = giveMe.AnApplicationContext();

        result.Add(content1);
        result.Add(content2);

        return result;
    }

    public static void ShouldHave<T>(this ApplicationContext context, T value)
    {
        context.Has<T>().ShouldBeTrue($"Context should have an item with type {typeof(T)}");
        context.Get<T>().ShouldBe(value);
    }

    public static void ShouldNotHave<T>(this ApplicationContext context, T value)
    {
        if (context.Has<T>())
        {
            context.Get<T>().ShouldNotBe(value);
        }
    }
}