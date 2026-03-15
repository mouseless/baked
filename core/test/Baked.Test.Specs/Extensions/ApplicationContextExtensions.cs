using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class ApplicationContextExtensions
{
    extension(Stubber giveMe)
    {
        public ApplicationContext AnApplicationContext() => new();

        public ApplicationContext AnApplicationContext<T>(T content) where T : notnull
        {
            var result = giveMe.AnApplicationContext();

            result.Add(content);

            return result;
        }

        public ApplicationContext AnApplicationContext<T1, T2>(T1 content1, T2 content2)
            where T1 : notnull
            where T2 : notnull
        {
            var result = giveMe.AnApplicationContext();

            result.Add(content1);
            result.Add(content2);

            return result;
        }
    }

    extension(ApplicationContext context)
    {
        public void ShouldHave<T>(T value)
        {
            context.Has<T>().ShouldBeTrue($"Context should have an item with type {typeof(T)}");
            context.Get<T>().ShouldBe(value);
        }

        public void ShouldNotHave<T>(T value)
        {
            if (context.Has<T>())
            {
                context.Get<T>().ShouldNotBe(value);
            }
        }
    }
}