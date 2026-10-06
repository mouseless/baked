using Baked.Business;
using Baked.ExceptionHandling;
using Baked.Playground.Orm;
using System.Runtime.CompilerServices;

namespace Baked.Playground;

public static class Exceptions
{
    public class UniqueException(string name)
        : HandledException("{0} should be unique",
            extraData: new() { [nameof(name)] = name }
        );

    public class MyChildException(Id id)
        : HandledException("Child#{0} does not belong this parent",
            extraData: new() { [nameof(id)] = id.ToString() }
        );

    extension(Validate validate)
    {
        public Validate Unique<T>(T? value, T? current, Func<T, object?> find,
            [CallerArgumentExpression(nameof(current))] string name = ""
        ) where T : struct =>
            validate.Unique<object>(value, current, v => find((T)v),
                name: name
            );

        public Validate Unique<T>(T? value, T? current, Func<T, object?> find,
            [CallerArgumentExpression(nameof(current))] string name = ""
        ) where T : class =>
            validate.That(l =>
            {
                if (value is null || value.Equals(current)) { return; }
                if (find(value) is null) { return; }

                throw new UniqueException(l(name));
            });

        public Validate MyChild(Parent parent, Child child) =>
            validate.That(() =>
            {
                if (parent == child.Parent) { return; }

                throw new MyChildException(child.Id);
            });
    }
}